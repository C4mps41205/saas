using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repository;
using Application.Validators;
using FluentValidation;

namespace Application.Usecases;

public class ClientsUsecase(
    IClientsRepository repository,
    IValidator<ClientRequest> createClientResponseValidator,
    IValidator<GetClientByIdRequest> getClientByIdRequestValidator,
    IValidator<GetClientRequest> getClientRequestValidator
    )
{
    #region --Queries

    public async Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest request)
    {
        var validator = getClientRequestValidator.Validate(request);

        if(!validator.IsValid)
            throw new ValidationException(validator.Errors);
        
        return await repository.GetPaginatedClients(request);
    }

    public GetClientResponse GetClientById(GetClientByIdRequest request)
    {
        var validator = getClientByIdRequestValidator.Validate(request);

        if(!validator.IsValid)
            throw new ValidationException(validator.Errors);
        
        return repository.GetClientById(request);
    }

    #endregion

    #region --Actions

    public CreateClientResponse CreateClient(ClientRequest request)
    {
        var validator = createClientResponseValidator.Validate(request);

        if(!validator.IsValid)
            throw new ValidationException(validator.Errors);
        
        return repository.CreateClient(request);
    }

    public bool UpdateClient(ClientRequest request, Guid id)
    {
        var validator = createClientResponseValidator.Validate(request);

        if(!validator.IsValid)
            throw new ValidationException(validator.Errors);
        
        return repository.UpdateClient(request, id);
    }

    public bool DeleteClient(Guid id)
    {
        return repository.DeleteClient(id);
    }

    #endregion
}