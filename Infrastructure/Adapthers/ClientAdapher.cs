using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using CRM_SAAS.Pages;
using Domain.Entitites;
using Infra.Data.DbContext;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Adapthers;

public class ClientAdapher(AppDbContext appDbContext) : IClientsRepository
{
    public PaginationDefault<GetClientResponse> GetPaginatedClients(GetClientRequest pagination)
    {
        int totalCount = appDbContext.Clients.Count();
        int totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);
        
        return new PaginationDefault<GetClientResponse>
        {
            Page = pagination.Page,
            PageSize = pagination.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Data = appDbContext.Clients
                .OrderBy(u => u.Id)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(u => new ClientsMapper().ToDto(u))
                .ToList()
        };
    }

    public CreateClientResponse CreateCategory(CreateClientRequest createClientRequest)
    {
        Client newClient = new CreateClientMapper().ToEntity(createClientRequest);
        EntityEntry<Client> createdCategory = appDbContext.Clients.Add(newClient);
        appDbContext.SaveChanges();

        return new CreateClientMapper().ToDto(createdCategory.Entity);
    }

    public UpdateClientResponse UpdateCategory(UpdateClientRequest categoryDto, Guid id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCategory(Guid id)
    {
        throw new NotImplementedException();
    }
}