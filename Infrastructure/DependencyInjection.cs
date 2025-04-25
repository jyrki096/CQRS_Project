using Infrastructure.Security.Extensions;
using Infrastructure.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddIdentityServices(configuration);
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqLiteConnection"));
        });

        return services;
    }
}
