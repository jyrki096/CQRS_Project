using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
