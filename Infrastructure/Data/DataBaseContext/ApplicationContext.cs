using Application.Data.DataBaseContext;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.DataBaseContext;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

    public ApplicationContext(DbContextOptions options) : base(options)
    {
        
    }
}
