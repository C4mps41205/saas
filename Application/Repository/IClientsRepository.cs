using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Repository;

public interface IClientsRepository
{
    PaginationDefault<GetClientResponse> GetPaginatedClients(GetClientRequest pagination);
    CreateClientResponse CreateCategory(CreateClientRequest createClientRequest);
    UpdateClientResponse UpdateCategory(UpdateClientRequest categoryDto, Guid id);
    bool DeleteCategory(Guid id);
    
}