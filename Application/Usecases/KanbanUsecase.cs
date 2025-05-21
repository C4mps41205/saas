using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repository;

namespace Application.Usecases;

public class KanbanUsecase(IKanbanRepository repository)
{
    #region --Queries

    public async Task<List<CardResponse>> GetAllKanban()
    {
        return await repository.GetAllKanban();
    }

    #endregion

    #region --Actions

    public async Task<bool> ChangeStatusCard(ChangeCardStatusRequest request) => await repository.ChangeStatusCard(request);
    
    
    public async Task<CreateCardKanbanResponse> CreateCardKanban(CreateCardKanbanRequest request)
    {
        return await repository.CreateCardKanban(request);
    }  
    
    public async Task<bool> UpdateCardKanban(UpdateCardKanbanRequest request)
    {
        return await repository.UpdateCardKanban(request);
    }   
    
    public async Task<bool> DeleteCardKanban(DeleteCardKanbanRequest request)
    {
        return await repository.DeleteCardKanban(request);
    }

    #endregion
}