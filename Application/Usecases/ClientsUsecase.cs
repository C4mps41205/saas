using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repository;
using Microsoft.AspNetCore.SignalR;

namespace Application.Usecases;

public class ClientsUsecase(IClientsRepository repository) : Hub
{
    public PaginationDefault<GetClientResponse> GetPaginatedClients(GetClientRequest request)
    {
        return repository.GetPaginatedClients(request);
    }
}