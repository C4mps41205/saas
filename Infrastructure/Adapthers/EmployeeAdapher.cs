using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using Domain.Entitites;
using Infra.Data.DbContext;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Adapthers;

public class EmployeeAdapher(AppDbContext appDbContext, IHubContext<EmployeeHub> hubContext) : IEmployeeRepository
{
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
        Employee? client = appDbContext.Employees.Find(getEmployeeByIdRequest.Guid);
        
        if(client == null)
            throw new ApplicationException("Employee not found");

        return new EmployeeMapper().ToDto(client);
    }

    #endregion

    #region --Actions

    public EmployeeResponse CreateEmployee(CreateEmployeeRequest employeeRequest)
    {
        Employee newEmployee = new CreateEmployeeMapper().ToEntity(employeeRequest);
        
        PasswordHasher<Employee> passwordHasher = new ();
        
        string passwordHash = passwordHasher.HashPassword(newEmployee, employeeRequest.Password!);
        byte[] passwordSalt = Convert.FromBase64String(passwordHash);
        
        newEmployee.PasswordHash = passwordHash;
        newEmployee.PasswordSalt = passwordSalt;
        
        EntityEntry<Employee> createdCategory = appDbContext.Employees.Add(newEmployee);
        appDbContext.SaveChanges();

        var dto = new CreateEmployeeMapper().ToDto(createdCategory.Entity);
        hubContext.Clients.All.SendAsync("EmployeeCreated", dto);
        return dto;
    }

    public bool UpdateEmployee(EmployeeRequest employeeDto, Guid id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEmployee(Guid id)
    {
        Employee? client = appDbContext.Employees.Find(id);
        
        if(client == null)
            throw new ApplicationException("Employee not found");
        
        appDbContext.Employees.Remove(client);
        appDbContext.SaveChanges();
        
        hubContext.Clients.All.SendAsync("EmployeeDeleted", id);
        return true;
    }

    #endregion
}