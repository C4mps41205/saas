using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc.DependencyInjection;

public static class AddSignalRDependencyInejction
{
    public static void AddSignalRDependency(this IServiceCollection services)
    {
        #region Service

        services.AddSignalR();

        #endregion
    }
}