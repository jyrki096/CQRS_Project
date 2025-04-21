using Application.Data.DataBaseContext;
using Infrastructure.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServices(configuration);
        services.AddScoped<IApplicationDbContext, ApplicationDbContext> ();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqLiteConnection"));
        });

        return services;
    }
}
