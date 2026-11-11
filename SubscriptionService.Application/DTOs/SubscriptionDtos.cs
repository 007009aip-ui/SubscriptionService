// File: src/SubscriptionService.Application/DTOs/SubscriptionDtos.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.DTOs;

/// <summary>POST /api/subscriptions payload. Type accepts "Basic" | "Premium" | "Corporate" (or 1 | 2 | 3).</summary>
public sealed record CreateSubscriptionRequest(SubscriptionType Type, DateTime StartDate, int BillingPeriodInMonths);

public sealed record SubscriptionDto(
    Guid Id,
    SubscriptionType Type,
    string TypeName,
    SubscriptionStatus Status,
    string StatusName,
    DateTime StartDate,
    int BillingPeriodInMonths,
    decimal PricePerPeriod,
    string Currency,
    DateTime CurrentPeriodEnd,
    DateTime? CancelledAtUtc,
    bool HasAccess);
