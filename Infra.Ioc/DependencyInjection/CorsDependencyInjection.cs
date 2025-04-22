using Microsoft.AspNetCore.Builder;

namespace Infra.Ioc.DependencyInjection;

public static class CorsDependencyInjection
{
    public static void AddCorsServices(this WebApplication app)
    {
        app.UseCors(x =>
        {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        });
    }
}