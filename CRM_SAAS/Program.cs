using Application.Dto.Request;
using CRM_SAAS.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IClientsRepository, ClientsServices>();
builder.Services.AddHttpClient<IClientsRepository, ClientsServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5159/");
});
builder.Services.AddSingleton<CreateClientResponse>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeServices>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();