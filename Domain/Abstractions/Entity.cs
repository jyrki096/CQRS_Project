using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }        
}
 