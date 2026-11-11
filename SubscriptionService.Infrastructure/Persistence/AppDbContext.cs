// File: src/SubscriptionService.Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Infrastructure.Persistence.Configurations;

namespace SubscriptionService.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // SQLite/InMemory return DateTimeKind.Unspecified; normalize to UTC so the same model
    // works with Npgsql 'timestamp with time column' without any change.
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
        configurationBuilder.Properties<DateTime?>().HaveConversion<NullableUtcDateTimeConverter>();
    }
}
