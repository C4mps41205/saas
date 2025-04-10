using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Usecases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CRM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController(ClientsUsecase usecase, IHubContext<ClientsUsecase> hubContext) : ControllerBase
{
    [HttpGet("GetPaginatedClients")]
    public IActionResult Get(GetClientRequest request)
    {
        try
        {
            PaginationDefault<GetClientResponse> response = usecase.GetPaginatedClients(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}