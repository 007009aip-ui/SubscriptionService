// File: src/SubscriptionService.Infrastructure/Persistence/DbInitializer.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Infrastructure.Persistence;

/// <summary>Creates the schema (demo mode) and seeds one demo user with a payment history.</summary>
public static class DbInitializer
{
    public const string DemoEmail = "demo@subscriptions.local";
    public const string DemoPassword = "Demo1234!";

    public static async Task InitializeAsync(
        AppDbContext db,
        IPasswordHasher passwordHasher,
        IClock clock,
        CancellationToken cancellationToken = default)
    {
        // Demo/dev shortcut. For PostgreSQL production use 'dotnet ef migrations add' + Database.Migrate().
        await db.Database.EnsureCreatedAsync(cancellationToken);

        if (await db.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var utcNow = clock.UtcNow;
        var monthStart = new DateTime(utcNow.Year, utcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var user = new User(Guid.NewGuid(), DemoEmail, passwordHasher.Hash(DemoPassword), "Demo User", utcNow);
        db.Users.Add(user);

        // Basic: started 5 months ago, renewed twice, cancelled 3 months ago (access ended 2 months ago).
        var basic = Subscription.Create(user.Id, SubscriptionType.Basic, monthStart.AddMonths(-5), 1, 10m, "USD");
        var payments = new List<Payment>
        {
            basic.CreateInitialPayment(monthStart.AddMonths(-5)),
            basic.Renew(monthStart.AddMonths(-4)),
            basic.Renew(monthStart.AddMonths(-3))
        };
        basic.Cancel(monthStart.AddMonths(-3));
        db.Subscriptions.Add(basic);

        // Premium: started 2 months ago, still active, paid for the current month.
        var premium = Subscription.Create(user.Id, SubscriptionType.Premium, monthStart.AddMonths(-2), 1, 25m, "USD");
        payments.Add(premium.CreateInitialPayment(monthStart.AddMonths(-2)));
        payments.Add(premium.Renew(monthStart.AddMonths(-1)));
        payments.Add(premium.Renew(monthStart));
        db.Subscriptions.Add(premium);

        db.Payments.AddRange(payments);
        await db.SaveChangesAsync(cancellationToken);
    }
}
