// File: src/SubscriptionService.Infrastructure/Common/SystemClock.cs
using SubscriptionService.Application.Abstractions;

namespace SubscriptionService.Infrastructure.Common;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
