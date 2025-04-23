using Application.Repository;
using Application.Usecases;
using Infrastructure.Adapthers;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc.DependencyInjection;

public static class InterfaceDependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        #region Adapthers

        services.AddScoped<IClientsRepository, ClientAdapher>();
        services.AddScoped<IEmployeeRepository, EmployeeAdapher>();

        #endregion

        #region Usecase

        services.AddScoped<ClientsUsecase>();
        services.AddScoped<EmployeeUsecase>();

        #endregion
    }
}