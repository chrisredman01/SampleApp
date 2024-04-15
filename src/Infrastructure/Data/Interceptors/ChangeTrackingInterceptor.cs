using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleApp.Domain.Common;

namespace SampleApp.Infrastructure.Data.Interceptors;

internal class ChangeTrackingInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetFields(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SetFields(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetFields(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var utcNow = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                SetCreated(entry.Entity, utcNow);
                SetModified(entry.Entity, utcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                SetModified(entry.Entity, utcNow);
            }
        }
    }

    private void SetCreated(object entity, DateTime date)
    {
        if (entity is ICreatableEntity creatableEntity)
        {
            creatableEntity.CreatedUtc = date;
        }
    }

    private void SetModified(object entity, DateTime date)
    {
        if (entity is IModifiableEntity modifiableEntity)
        {
            modifiableEntity.ModifiedUtc = date;
        }
    }
}