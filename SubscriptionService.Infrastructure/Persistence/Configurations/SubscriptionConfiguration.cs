// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/SubscriptionConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

public sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Type).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(s => s.PricePerPeriod).HasPrecision(18, 2);
        builder.Property(s => s.Currency).HasMaxLength(3).IsRequired();
        builder.Property(s => s.StartDate).IsRequired();
        builder.Property(s => s.CurrentPeriodEnd).IsRequired();
        builder.HasIndex(s => new { s.UserId, s.Status });
        builder.HasIndex(s => s.CurrentPeriodEnd);

        builder.HasMany(s => s.Payments)
            .WithOne(p => p.Subscription)
            .HasForeignKey(p => p.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(s => s.Payments).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
