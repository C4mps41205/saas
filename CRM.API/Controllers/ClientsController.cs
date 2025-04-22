using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController(ClientsUsecase usecase) : ControllerBase
{
    #region --Queries

    [HttpGet("GetPaginatedClients")]
    public async Task<ActionResult<PaginationDefault<GetClientResponse>>> Get([FromQuery] GetClientRequest request)
    {
        try
        {
            PaginationDefault<GetClientResponse> response = await usecase.GetPaginatedClients(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("GetClientById")]
    public ActionResult GetById([FromQuery] GetClientByIdRequest request)
    {
        try
        {
            GetClientResponse response = usecase.GetClientById(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }   

    #endregion

    #region --Actions

    [HttpPost("CreateClient")]
    public ActionResult Create([FromBody] ClientRequest request)
    {
        try
        {
            usecase.CreateClient(request);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }    
    
    [HttpPatch("UpdateClient")]
    public ActionResult Update([FromQuery] Guid id, [FromBody] ClientRequest request)
    {
        try
        {
            usecase.UpdateClient(request, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("DeleteClient")]
    public ActionResult Delete([FromQuery] Guid id)
    {
        try
        {
            usecase.DeleteClient(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion
}