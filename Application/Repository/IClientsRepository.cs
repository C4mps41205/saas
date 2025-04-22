using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Repository;

public interface IClientsRepository
{
    Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest pagination);
    GetClientResponse GetClientById(GetClientByIdRequest getClientByIdRequest);
    CreateClientResponse CreateClient(ClientRequest clientRequest);
    bool UpdateClient(ClientRequest clientDto, Guid id);
    bool DeleteClient(Guid id);
}