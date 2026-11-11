// File: src/SubscriptionService.Application/Pricing/SubscriptionPricing.cs
using Microsoft.Extensions.Options;
using SubscriptionService.Application.Options;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Pricing;

public sealed class SubscriptionPricing : ISubscriptionPricing
{
    private readonly PricingOptions _options;

    public SubscriptionPricing(IOptions<PricingOptions> options) => _options = options.Value;

    public string Currency => _options.Currency;

    public decimal GetMonthlyPrice(SubscriptionType type) => type switch
    {
        SubscriptionType.Basic => _options.BasicMonthlyPrice,
        SubscriptionType.Premium => _options.PremiumMonthlyPrice,
        SubscriptionType.Corporate => _options.CorporateMonthlyPrice,
        _ => throw new ValidationException(nameof(SubscriptionType), $"Unknown subscription type '{type}'.")
    };
}
