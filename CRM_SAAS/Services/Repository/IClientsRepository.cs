using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace CRM_SAAS.Services.Repository;

public interface IClientsRepository
{
    event Action<CreateClientResponse>? OnClientsCreated;
    event Action<bool>? OnClientDeleted;

    Task InitializeConnectionHubClient();
    Task DisconnectAsync();
    Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest request);
    Task<CreateClientResponse> CreateClients(ClientRequest request);
    Task<bool> UpdateClients(ClientRequest request, Guid Id);
    Task<HttpResponseMessage> DeleteClients(Guid Id);
    Task<GetClientResponse> GetClientsById(GetClientByIdRequest request);
}