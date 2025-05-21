using Application.Dto.Request;
using Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class KanbanController(KanbanUsecase useCase) : ControllerBase
{
    #region --Queries

    [HttpGet("GetAllKanban")]
    public async Task<IActionResult> GetAllKanban()
    {
        var kanban = await useCase.GetAllKanban();
        return Ok(kanban);
    }

    #endregion

    #region --Actions
    
    [HttpPost("CreateCard")]
    public async Task<IActionResult> CreateCardKanban([FromBody] CreateCardKanbanRequest request)
    {
        await useCase.CreateCardKanban(request);
        return Created();
    }
    
    [HttpPost("ChangeStatusCard")]
    public async Task<IActionResult> ChangeStatusCard([FromBody] ChangeCardStatusRequest request) 
        => Ok(await useCase.ChangeStatusCard(request));
    
    [HttpPut("UpdateCard")]
    public async Task<IActionResult> UpdateCardKanban([FromBody] UpdateCardKanbanRequest request)
    {
        var kanban = await useCase.UpdateCardKanban(request);
        return Ok(kanban);
    }    
    
    [HttpDelete("DeleteCardKanban")]
    public async Task<IActionResult> DeleteCardKanban([FromQuery] DeleteCardKanbanRequest request)
    {
        var kanban = await useCase.DeleteCardKanban(request);
        return Ok(kanban);
    }

    #endregion
}