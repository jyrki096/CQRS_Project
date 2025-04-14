using Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var manager = scope.ServiceProvider.GetRequiredService<UserManager<CustomIdentityUser>>();

        dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedData(dbContext, manager);
    }

    private static async Task SeedData(ApplicationDbContext dbContext, UserManager<CustomIdentityUser> manager)
    {
        await SeedTopicAsync(dbContext);
        await SeedUsersAsync(dbContext, manager);
    }

    private static async Task SeedUsersAsync(ApplicationDbContext dbContext, UserManager<CustomIdentityUser> manager)
    {
        if(!manager.Users.Any())
        {
            foreach (var user in InitialData.Users)
            {
                await manager.CreateAsync(user, "qwerty123");
            }
        }
    }


    private static async Task SeedTopicAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Topics.AnyAsync())
        {
            await dbContext.Topics.AddRangeAsync(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }
}
