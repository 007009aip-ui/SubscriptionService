// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/UtcDateTimeConverter.cs
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

/// <summary>Guarantees that every DateTime leaving/entering the database is DateTimeKind.Utc.</summary>
public sealed class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter()
        : base(value => DateTime.SpecifyKind(value, DateTimeKind.Utc),
               value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
    {
    }
}

public sealed class NullableUtcDateTimeConverter : ValueConverter<DateTime?, DateTime?>
{
    public NullableUtcDateTimeConverter()
        : base(value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value,
               value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value)
    {
    }
}
