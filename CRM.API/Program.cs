var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddSignalRDependency();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });;
builder.Services.AddDependencyInjection();

var app = builder.Build();

app.MapSignalR();
app.MapOpenApi();
app.AddCorsServices();
app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();