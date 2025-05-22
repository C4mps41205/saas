using Application.Dto.Response;
using Application.Repository;

namespace Application.Usecases;

public class ReportUseCase(IReportRepository repository)
{
    public async Task<ReportClientsCurrentMonthResponse> GetReportClientsCurrentMonth()
    {
        return await repository.GetReportClientsCurrentMonth();
    }
}