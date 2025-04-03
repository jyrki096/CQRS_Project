using Infrastructure.Data.DataBaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedData(dbContext);
    }

    private static async Task SeedData(ApplicationDbContext dbContext)
    {
        await SeedTopicAsync(dbContext);
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
