using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Repository;

public interface IEmployeeRepository
{
    Task<PaginationDefault<EmployeeResponse>> GetPaginatedEmployees(GetEmployeeRequest pagination);
    GetEmployeeResponse GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest);
    CreateEmployeeResponse CreateEmployee(EmployeeRequest employeeRequest);
    bool UpdateEmployee(EmployeeRequest employeeDto, Guid id);
    bool DeleteEmployee(Guid id);
}