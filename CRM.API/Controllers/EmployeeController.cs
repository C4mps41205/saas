using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(EmployeeUsecase usecase) : ControllerBase
{
    #region --Queries

    [HttpGet("GetPaginatedEmployees")]
    public async Task<ActionResult<PaginationDefault<GetEmployeeResponse>>> Get([FromQuery] GetEmployeeRequest request)
    {
        try
        {
            PaginationDefault<EmployeeResponse> response = await usecase.GetPaginatedEmployees(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("GetEmployeeById")]
    public ActionResult GetById([FromQuery] GetEmployeeByIdRequest request)
    {
        try
        {
            EmployeeResponse response = usecase.GetEmployeeById(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }   

    #endregion

    #region --Actions

    [HttpPost("CreateEmployee")]
    public ActionResult Create([FromBody] CreateEmployeeRequest request)
    {
        try
        {
            usecase.CreateEmployee(request);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }    
    
    [HttpPatch("UpdateEmployee")]
    public ActionResult Update([FromQuery] Guid id, [FromBody] EmployeeRequest request)
    {
        try
        {
            usecase.UpdateEmployee(request, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("DeleteEmployee")]
    public ActionResult Delete([FromQuery] Guid id)
    {
        try
        {
            usecase.DeleteEmployee(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion
}