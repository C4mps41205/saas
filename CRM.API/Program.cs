var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddSignalRDependency();
var app = builder.Build();
app.MapSignalR();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors(x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();