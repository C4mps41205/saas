using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using Domain.Entitites;
using Infra.Data.DbContext;
using Infrastructure.Hubs;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Adapthers;

public class EmployeeAdapher(
    AppDbContext appDbContext,
    IHubContext<EmployeeHub> hubContext,
    IEmailService emailService,
    JwtUtil jwtUtils,
    IConfiguration configuration)
    : IEmployeeRepository
{

    #region --Auth

    public Task<AuthEmployeeResponse> Authenticate(AuthEmployeeRequest request)
    {
        var user = appDbContext.Employees.SingleOrDefault(x => x.CorporateEmail == request.Email);

        if (user == null)
            throw new ApplicationException("Email or password is incorrect");

        PasswordHasher<Employee> passwordHasher = new();

        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) !=
            PasswordVerificationResult.Success)
            throw new ApplicationException("Email or password is incorrect");
        
        var token = jwtUtils.GenerateToken(user.CorporateEmail);

        return Task.FromResult(new AuthEmployeeResponse
        {
            Token = token,
            Employee = new EmployeeMapper().ToDto(user)
        });
    }

    public async Task<bool> ResetPassword(ResetPasswordRequest request)
    {
        var user = appDbContext.Employees.FirstOrDefault(x => x.Id == request.Id);
        
        if(user == null)
            throw new ApplicationException("Email not found");
        
        PasswordHasher<Employee> passwordHasher = new();
        
        string passwordHash = passwordHasher.HashPassword(user, request.Password);
        byte[] passwordSalt = Convert.FromBase64String(passwordHash);
        
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        appDbContext.SaveChanges();
        
        Dictionary<string, string> replacements = new Dictionary<string, string>()
        {
            { "Name", user.Name },
            { "Email", user.CorporateEmail },
            { "Password", request.Password },
            { "LinkERP", configuration["FrontendUrl"] ?? throw new ArgumentNullException(nameof(configuration)) },
            { "Year", new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString() }
        };

        string template =
            await emailService.ReplaceEmailParams(
                (configuration["TemplatesUrl"] ?? throw new ArgumentException($"Not found variable TemplatesUrl")),
                "ResetPassword.html", replacements);
        
        emailService.Execute(template, "Your password has been updated", user.Email);
        
        return true;
    }

    #endregion
    
    #region --Queries

    public Task<PaginationDefault<EmployeeResponse>> GetPaginatedEmployees(GetEmployeeRequest pagination)
    {
        int totalCount = appDbContext.Clients.Count();
        int totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

        return Task.FromResult(new PaginationDefault<EmployeeResponse>
        {
            Page = pagination.Page,
            PageSize = pagination.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Data = appDbContext.Employees
                .OrderBy(u => u.Id)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(u => new EmployeeMapper().ToDto(u))
                .ToList()
        });
    }

    public EmployeeResponse GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
    {
        Employee? client = appDbContext.Employees.Find(getEmployeeByIdRequest.Id);

        if (client == null)
            throw new ApplicationException("Employee not found");

        return new EmployeeMapper().ToDto(client);
    }
    
    #endregion

    #region --Actions

    public async Task<EmployeeResponse> CreateEmployee(CreateEmployeeRequest employeeRequest)
    {
        Employee newEmployee = new CreateEmployeeMapper().ToEntity(employeeRequest);

        PasswordHasher<Employee> passwordHasher = new();

        string passwordHash = passwordHasher.HashPassword(newEmployee, employeeRequest.Password);
        byte[] passwordSalt = Convert.FromBase64String(passwordHash);

        newEmployee.PasswordHash = passwordHash;
        newEmployee.PasswordSalt = passwordSalt;

        EntityEntry<Employee> createdCategory = appDbContext.Employees.Add(newEmployee);
        appDbContext.SaveChanges();

        Dictionary<string, string> replacements = new Dictionary<string, string>()
        {
            { "Name", newEmployee.Name },
            { "Email", newEmployee.CorporateEmail },
            { "Password", employeeRequest.Password },
            { "LinkERP", configuration["FrontendUrl"] ?? throw new ArgumentNullException(nameof(configuration)) },
            { "Year", new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString() }
        };

        string template =
            await emailService.ReplaceEmailParams(
                (configuration["TemplatesUrl"] ?? throw new ArgumentException($"Not found variable TemplatesUrl")),
                "CreatedUser.html", replacements);
        emailService.Execute(template, "New user created", newEmployee.Email);

        var dto = new CreateEmployeeMapper().ToDto(createdCategory.Entity);
        await hubContext.Clients.All.SendAsync("EmployeeCreated", dto);
        return dto;
    }

    public bool UpdateEmployee(CreateEmployeeRequest employeeDto, Guid id)
    {
        var employee = appDbContext.Employees.Find(id);

        if (employee == null)
            throw new ApplicationException("Employee not found");

        employee.Name = employeeDto.Name;
        employee.CPF = employeeDto.CPF;
        employee.SimultaneousServices = employeeDto.SimultaneousServices;
        employee.Email = employeeDto.Email;
        employee.CorporateEmail = employeeDto.CorporateEmail;
        employee.Phone = employeeDto.Phone;

        appDbContext.SaveChanges();

        hubContext.Clients.All.SendAsync("EmployeeUpdated", new EmployeeMapper().ToDto(employee));
        return true;
    }

    public bool DeleteEmployee(Guid id)
    {
        Employee? client = appDbContext.Employees.Find(id);

        if (client == null)
            throw new ApplicationException("Employee not found");

        appDbContext.Employees.Remove(client);
        appDbContext.SaveChanges();

        hubContext.Clients.All.SendAsync("EmployeeDeleted", id);
        return true;
    }

    #endregion
}