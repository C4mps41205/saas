using CRM_SAAS.Services;
using CRM_SAAS.Services.Repository;

namespace CRM_SAAS.DependencyInjection;

public static class ClientDependencyInjection
{
    public static void AddCustomHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IClientsRepository, ClientsServices>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5159/");
        });
        
        services.AddHttpClient<IEmployeeRepository, EmployeeServices>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5159/");
        });
    }
}