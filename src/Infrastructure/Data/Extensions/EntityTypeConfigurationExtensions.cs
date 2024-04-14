using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApp.Domain.Common;

namespace SampleApp.Infrastructure.Data.Extensions;

internal static class EntityTypeConfigurationExtensions
{
    internal static EntityTypeBuilder<TEntity> IsEntity<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity<TKey>
    {
        builder.HasKey(entity => entity.Id);

        builder.Ignore(entity => entity.DomainEvents);

        return builder;
    }

    internal static EntityTypeBuilder<TEntity> IsEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity
    {
        return builder.IsEntity<TEntity, Guid>();
    }
}
