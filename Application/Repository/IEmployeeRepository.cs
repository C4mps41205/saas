using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Repository;

public interface IEmployeeRepository
{
    Task<PaginationDefault<EmployeeResponse>> GetPaginatedEmployees(GetEmployeeRequest pagination);
    EmployeeResponse GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest);
    Task<EmployeeResponse> CreateEmployee(CreateEmployeeRequest employeeRequest);
    bool UpdateEmployee(CreateEmployeeRequest employeeDto, Guid id);
    bool DeleteEmployee(Guid id);
}