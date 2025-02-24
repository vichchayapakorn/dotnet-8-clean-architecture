using System;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IOrderDbContext
{

    public DbSet<Order> Orders { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
