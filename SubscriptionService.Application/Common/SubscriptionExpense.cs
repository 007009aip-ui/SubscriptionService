// File: src/SubscriptionService.Application/Common/SubscriptionExpense.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.Common;

/// <summary>Denormalized payment row used by the analytics service (EF translates the projection to SQL).</summary>
public sealed record SubscriptionExpense(Guid SubscriptionId, SubscriptionType Type, decimal Amount, DateTime PaidAtUtc);
