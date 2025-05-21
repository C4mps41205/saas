using Application.Repository;
using Application.Usecases;
using Infrastructure.Adapthers;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc.DependencyInjection;

public static class InterfaceDependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        #region Adapthers

        services.AddScoped<IClientsRepository, ClientAdapher>();
        services.AddScoped<IEmployeeRepository, EmployeeAdapher>();
        services.AddScoped<IKanbanRepository, KanbanAdapther>();

        #endregion

        #region Usecase

        services.AddScoped<ClientsUsecase>();
        services.AddScoped<EmployeeUsecase>();
        services.AddScoped<KanbanUsecase>();

        #endregion

        #region Services

        services.AddScoped<IEmailService, EmailService>();

        #endregion
    }
}