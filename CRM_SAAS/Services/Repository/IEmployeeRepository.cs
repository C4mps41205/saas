using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace CRM_SAAS.Services.Repository;

public interface IEmployeeRepository
{
    event Action<CreateEmployeeResponse>? OnEmployeesCreated;
    event Action<bool>? OnEmployeeDeleted;

    Task InitializeConnectionHubEmployee();
    Task DisconnectAsync();
    Task<PaginationDefault<GetEmployeeResponse>> GetPaginatedEmployees(EmployeeRequest request);
    Task<bool> CreateEmployees(CreateEmployeeRequest request);
    Task<bool> UpdateEmployees(CreateEmployeeRequest request, Guid Id);
    Task<HttpResponseMessage> DeleteEmployees(Guid Id);
    Task<GetEmployeeResponse> GetEmployeesById(GetEmployeeByIdRequest request);
}