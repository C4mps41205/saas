using Application.Dto.Request;
using Application.Dto.Response;

namespace CRM_SAAS.Services.Repository;

public interface IKanbanRepository
{
   event Action<bool>? OnCardsCreated;
   event Action<bool>? OnCardsUpdated;
   event Action<bool>? OnCardDeleted;

   public Task InitializeConnectionHubKanban();
   public Task DisconnectAsync();
    Task<CardResponse?> GetCardById(Guid id);
    Task<List<CardResponse>> GetKanban(GetCardRequest request);
    Task<bool> CreateCard(CreateCardKanbanRequest request);
    Task<bool> UpdateCard(UpdateCardKanbanRequest request, Guid id);
    Task<bool> ChangeStatusCard(ChangeCardStatusRequest request);
    Task<bool> DeleteCard(Guid id);
}