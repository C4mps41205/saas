using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Repository;

public interface IKanbanRepository
{
    Task<List<CardResponse>> GetAllKanban();
    Task<bool> ChangeStatusCard(ChangeCardStatusRequest request);
    Task<CreateCardKanbanResponse> CreateCardKanban(CreateCardKanbanRequest request);
    Task<bool> UpdateCardKanban(UpdateCardKanbanRequest request);
    Task<bool> DeleteCardKanban(DeleteCardKanbanRequest request);
}