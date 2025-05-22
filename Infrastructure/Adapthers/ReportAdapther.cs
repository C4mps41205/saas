using Application.Dto.Response;
using Application.Repository;
using Infra.Data.DbContext;

namespace Infrastructure.Adapthers;

public class ReportAdapther(AppDbContext dbContext) : IReportRepository
{
    #region --Dashboard

    public Task<ReportClientsCurrentMonthResponse> GetReportClientsCurrentMonth()
    {
        var clients = dbContext.Clients.Where(x => x.CreatedAt.Month == DateTime.Now.Month);
        
        return Task.FromResult(new ReportClientsCurrentMonthResponse());
    }

    #endregion
}