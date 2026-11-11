// File: src/SubscriptionService.Application/Pricing/ISubscriptionPricing.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.Pricing;

/// <summary>Single source of truth for prices (Open/Closed: a new plan does not touch the service logic).</summary>
public interface ISubscriptionPricing
{
    string Currency { get; }

    decimal GetMonthlyPrice(SubscriptionType type);
}
