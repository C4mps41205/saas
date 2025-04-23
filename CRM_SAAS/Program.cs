using Application.Dto.Request;
using CRM_SAAS.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IClientsRepository, ClientsServices>();
builder.Services.AddCustomHttpClient(builder.Configuration);

await builder.Build().RunAsync();