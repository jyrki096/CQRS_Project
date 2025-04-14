using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Logging.AddConsole();

builder.Services.AddApiServices(builder.Configuration)
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration);
                

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseApiServices();
app.Run();
