using Api.Exceptions.Handler;
using Api.Middleware;
using Api.Security.Extensions;
using Api.Security.Services;
using Application.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

            options.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddOpenApi();
        services.AddIdentityServices(configuration);
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();


        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(typeof(GetTopicsHandler).Assembly));

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseMiddleware<ValidationMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler(options => { });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
