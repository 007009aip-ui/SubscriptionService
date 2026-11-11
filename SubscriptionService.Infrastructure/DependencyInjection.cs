// File: src/SubscriptionService.Infrastructure/DependencyInjection.cs
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Infrastructure.Common;
using SubscriptionService.Infrastructure.Persistence;
using SubscriptionService.Infrastructure.Persistence.Repositories;
using SubscriptionService.Infrastructure.Security;

namespace SubscriptionService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        if (!Enum.TryParse<DatabaseProvider>(options.Provider, ignoreCase: true, out var provider))
        {
            throw new InvalidOperationException(
                $"Unknown database provider '{options.Provider}'. Allowed: {string.Join(", ", Enum.GetNames<DatabaseProvider>())}.");
        }

        switch (provider)
        {
            case DatabaseProvider.SqliteInMemory:
                // Keep one open connection for the whole process: closing it would drop the in-memory database.
                services.AddSingleton(_ =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    return connection;
                });
                services.AddDbContext<AppDbContext>((sp, o) =>
                    o.UseSqlite(sp.GetRequiredService<SqliteConnection>()));
                break;

            case DatabaseProvider.InMemory:
                services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("SubscriptionService"));
                break;

            case DatabaseProvider.Postgres:
                if (string.IsNullOrWhiteSpace(options.ConnectionString))
                {
                    throw new InvalidOperationException("Database:ConnectionString is required for the Postgres provider.");
                }

                services.AddDbContext<AppDbContext>(o => o.UseNpgsql(options.ConnectionString));
                break;
        }

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IPasswordHasher, IdentityPasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
