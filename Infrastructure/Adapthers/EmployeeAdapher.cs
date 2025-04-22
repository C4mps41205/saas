using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using Infra.Data.DbContext;

namespace Infrastructure.Adapthers;

public class EmployeeAdapher(AppDbContext appDbContext) : IEmployeeRepository
{
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

    public GetEmployeeResponse GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
    {
        throw new NotImplementedException();
    }

    public CreateEmployeeResponse CreateEmployee(EmployeeRequest employeeRequest)
    {
        throw new NotImplementedException();
    }

    public bool UpdateEmployee(EmployeeRequest employeeDto, Guid id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEmployee(Guid id)
    {
        throw new NotImplementedException();
    }
}