using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
