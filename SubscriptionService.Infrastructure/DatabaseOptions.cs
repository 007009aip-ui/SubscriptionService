// File: src/SubscriptionService.Infrastructure/DatabaseOptions.cs
namespace SubscriptionService.Infrastructure;

public enum DatabaseProvider
{
    /// <summary>Default: real relational SQL + transactions, zero setup, data lives while the process runs.</summary>
    SqliteInMemory,

    /// <summary>EF InMemory provider (no transactions, no SQL).</summary>
    InMemory,

    /// <summary>Production provider - only a connection string change is required.</summary>
    Postgres
}

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public string Provider { get; set; } = nameof(DatabaseProvider.SqliteInMemory);

    public string? ConnectionString { get; set; }

    public bool SeedDemoData { get; set; } = true;
}
