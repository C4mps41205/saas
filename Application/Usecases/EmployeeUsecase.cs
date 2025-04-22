using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repository;

namespace Application.Usecases;

public class EmployeeUsecase(IEmployeeRepository repository)
{
    #region --Queries

    public async Task<PaginationDefault<EmployeeResponse>> GetPaginatedEmployees(GetEmployeeRequest request)
    {
        return await repository.GetPaginatedEmployees(request);
    }
    
    public GetEmployeeResponse GetEmployeeById(GetEmployeeByIdRequest request)
    {
        return repository.GetEmployeeById(request);
    }   

    #endregion

    #region --Actions

    public CreateEmployeeResponse CreateEmployee(EmployeeRequest request)
    {
        return repository.CreateEmployee(request);
    }

    public bool UpdateEmployee(EmployeeRequest request, Guid id)
    {
        return repository.UpdateEmployee(request, id);
    }

    public bool DeleteEmployee(Guid id)
    {
        return repository.DeleteEmployee(id);
    }

    #endregion
}