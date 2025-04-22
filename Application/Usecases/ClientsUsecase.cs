using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repository;

namespace Application.Usecases;

public class ClientsUsecase(IClientsRepository repository)
{
    #region --Queries

    public async Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest request)
    {
        return await repository.GetPaginatedClients(request);
    }
    
    public GetClientResponse GetClientById(GetClientByIdRequest request)
    {
        return repository.GetClientById(request);
    }   

    #endregion

    #region --Actions

    public CreateClientResponse CreateClient(ClientRequest request)
    {
        return repository.CreateClient(request);
    }

    public bool UpdateClient(ClientRequest request, Guid id)
    {
        return repository.UpdateClient(request, id);
    }

    public bool DeleteClient(Guid id)
    {
        return repository.DeleteClient(id);
    }

    #endregion
}