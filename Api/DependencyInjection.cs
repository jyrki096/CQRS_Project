using Api.Exceptions.Handler;
using Application.Security.Services;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Net;

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
        
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();


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

        app.UseStatusCodePages(async context =>
        {
            if (context.HttpContext.Response.StatusCode == 403)
            {
                var details = new ProblemDetails
                {
                    Title = "Forbidden",
                    Detail = "У вас недостаточно прав для этого действия",
                    Status = StatusCodes.Status403Forbidden,
                    Instance = context.HttpContext.Request.Path
                };

                details.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);


                await context.HttpContext.Response.WriteAsJsonAsync(details);
            };
        });

        app.UseExceptionHandler(options => { });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
