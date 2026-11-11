// File: src/SubscriptionService.Application/Options/PricingOptions.cs
namespace SubscriptionService.Application.Options;

/// <summary>Monthly prices per plan. Bound from configuration section "Pricing".</summary>
public sealed class PricingOptions
{
    public const string SectionName = "Pricing";

    public decimal BasicMonthlyPrice { get; set; } = 10m;

    public decimal PremiumMonthlyPrice { get; set; } = 25m;

    public decimal CorporateMonthlyPrice { get; set; } = 100m;

    public string Currency { get; set; } = "USD";
}
