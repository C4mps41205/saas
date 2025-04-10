using Application.Usecases;
using Microsoft.AspNetCore.SignalR;

namespace CRM.API.Controllers;

public static class ControllerSignalR
{
    public static void MapSignalR(this WebApplication app)
    {
        app.MapHub<ClientsUsecase>("Clients");
    }
}