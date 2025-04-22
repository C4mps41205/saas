using Infrastructure.Hubs;

namespace CRM.API.Controllers;

internal static class ControllerSignalR
{
    public static void MapSignalR(this WebApplication app)
    {
        app.MapHub<ClientHub>("/ClientsHub");
        app.MapHub<EmployeeHub>("/EmployeeHub");
    }
}