using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Data.Extensions;

namespace SampleApp.Infrastructure.Data.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .IsEntity();

        builder
            .HasIndex(entity => entity.Name)
            .IsUnique();

        builder
            .Property(entity => entity.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(entity => entity.Telephone)
            .HasMaxLength(20);

        builder
            .Property(entity => entity.Email)
            .HasMaxLength(320);

        builder
            .ToTable("Customers");
    }
}
