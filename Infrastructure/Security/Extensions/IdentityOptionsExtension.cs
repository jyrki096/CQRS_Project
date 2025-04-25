using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infrastructure.Security.Extensions;

public static class IdentityOptionsExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<CustomIdentityUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        var secretKey = configuration["AuthSettings:SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

        services.AddTransient<IAuthorizationHandler, TopicDeletionRequirementHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsTopicAuthor", policy =>
            {
                policy.Requirements.Add(new TopicDeletionRequirement());
            });
        });

        

        return services;      
    }
}
