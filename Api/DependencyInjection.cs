using Api.Exceptions.Handler;
using Api.Security.Extensions;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi();
        services.AddIdentityServices(configuration);


        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(typeof(GetTopicsHandler).Assembly));

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler(options => { });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
