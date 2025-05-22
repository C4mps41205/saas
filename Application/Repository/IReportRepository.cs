using Application.Dto.Response;

namespace Application.Repository;

public interface IReportRepository
{
    Task<ReportClientsCurrentMonthResponse> GetReportClientsCurrentMonth();
}