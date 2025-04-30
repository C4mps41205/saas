using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using Domain.Entitites;
using Infra.Data.DbContext;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Adapthers;

public class ClientAdapher(AppDbContext appDbContext, IHubContext<ClientHub> hubContext) : IClientsRepository
{
    #region --Queries

    public Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest pagination)
    {
        int totalCount = appDbContext.Clients.Count();
        int totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

        return Task.FromResult(new PaginationDefault<GetClientResponse>
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
        });
    }

    public GetClientResponse GetClientById(GetClientByIdRequest getClientByIdRequest)
    {
        Client? client = appDbContext.Clients.Find(getClientByIdRequest.Id);

        if (client == null)
            throw new ApplicationException("Client not found");

        return new ClientsMapper().ToDto(client);
    }

    #endregion

    #region --Actions

    public CreateClientResponse CreateClient(ClientRequest clientRequest)
    {
        Client newClient = new CreateClientMapper().ToEntity(clientRequest);
        EntityEntry<Client> createdCategory = appDbContext.Clients.Add(newClient);
        appDbContext.SaveChanges();

        var dto = new CreateClientMapper().ToDto(createdCategory.Entity);
        hubContext.Clients.All.SendAsync("ClientCreated", dto);
        return dto;
    }

    public bool UpdateClient(ClientRequest clientDto, Guid id)
    {
        var client = appDbContext.Clients.Find(id);
        
        if(client == null)
            throw new ApplicationException("Client not found");

        client.Phone = clientDto.Phone;
        client.Name = clientDto.Name;
        client.CpfCnpj = clientDto.CpfCnpj;
        client.PersonType = clientDto.PersonType;
        client.Email = clientDto.Email;
        client.BirthDate = clientDto.BirthDate;
        client.State = clientDto.State;
        client.City = clientDto.City;
        client.Cep = clientDto.Cep;
        client.Number = clientDto.Number;
        client.Neighborhood = clientDto.Neighborhood;
        client.Complement = clientDto.Complement;
        client.Subordinates = appDbContext.Clients.Where(x => (clientDto.Subordinates ?? new List<Guid>()).Contains(x.Id)).ToList();

        appDbContext.SaveChanges();

        hubContext.Clients.All.SendAsync("ClientUpdated", new CreateClientMapper().ToDto(client));
        return true;
    }

    public bool DeleteClient(Guid id)
    {
        Client? client = appDbContext.Clients.Find(id);

        if (client == null)
            throw new ApplicationException("Client not found");

        appDbContext.Clients.Remove(client);
        appDbContext.SaveChanges();

        hubContext.Clients.All.SendAsync("ClientDeleted", id);
        return true;
    }

    #endregion
}