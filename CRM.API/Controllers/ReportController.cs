using Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController(ReportUseCase reportUseCase) : ControllerBase
{
    public async Task<IActionResult> GetReportClientsCurrentMonth() =>
        Ok(await reportUseCase.GetReportClientsCurrentMonth());
}