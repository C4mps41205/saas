using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper;
using Application.Repository;
using Infra.Data.DbContext;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapthers;

public class KanbanAdapther(AppDbContext appDbContext, IHubContext<CardHub> hubContext) : IKanbanRepository
{
    #region --Queries

    public Task<List<CardResponse>> GetAllKanban() =>
        appDbContext.Cards.OrderBy(x => x.CreatedAt)
            .Include(x => x.Employee)
            .Select(x => new CardMapper().ToDto(x))
            .ToListAsync();

    public async Task<CardResponse> GetById(GetCardByIdRequest request)
    {
        var card = await appDbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (card != null)
        {
            var cardDto = new CardMapper().ToDto(card);
            return cardDto;
        }

        return new CardResponse();
    }

    #endregion

    #region --Actions

    public Task<bool> ChangeStatusCard(ChangeCardStatusRequest request)
    {
        var card = appDbContext.Cards.FirstOrDefault(x => x.Id == request.Id)
                   ?? throw new Exception("Card not found");
        card.KanbanStatusCard = request.KanbanStatusCard;

        appDbContext.SaveChanges();
        hubContext.Clients.All.SendAsync("UpdatedCardKanban", true);

        return Task.FromResult(true);
    }

    public Task<CreateCardKanbanResponse> CreateCardKanban(CreateCardKanbanRequest request)
    {
        var card = new CreateCardMapper().ToEntity(request);
        card.Clients = appDbContext.Clients.Where(x => request.Clients.Contains(x.Id)).ToList();
        card.Employee = appDbContext.Employees.First(x => x.Id == request.Employee) ??
                        throw new Exception("Employee not found");

        appDbContext.Cards.Add(card);
        appDbContext.SaveChanges();

        hubContext.Clients.All.SendAsync("CreatedCardKanban", true);

        return Task.FromResult(new CreateCardMapper().ToDto(card));
    }

    public Task<bool> UpdateCardKanban(UpdateCardKanbanRequest request)
    {
        var card = appDbContext.Cards.FirstOrDefault(x => x.Id == request.Id)
                   ?? throw new Exception("Card not found");
        card.Title = request.Title;
        card.Description = request.Description;
        card.KanbanStatusCard = request.KanbanStatusCard;
        card.Clients = appDbContext.Clients.Where(x => request.Clients.Contains(x.Id)).ToList();
        card.Employee = appDbContext.Employees.FirstOrDefault(x => x.Id == request.Employee) ??
                        throw new Exception("Employee not found");

        appDbContext.SaveChanges();
        hubContext.Clients.All.SendAsync("UpdatedCardKanban", true);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteCardKanban(DeleteCardKanbanRequest request)
    {
        var card = appDbContext.Cards.FirstOrDefault(x => x.Id == request.Id)
                   ?? throw new Exception("Card not found");
        appDbContext.Cards.Remove(card);
        appDbContext.SaveChanges();
        hubContext.Clients.All.SendAsync("DeletedCardKanban", true);
        return Task.FromResult(true);
    }

    #endregion
}