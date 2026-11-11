// File: src/SubscriptionService.Application/DependencyInjection.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.Options;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Application.Services;

namespace SubscriptionService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PricingOptions>()
            .Bind(configuration.GetSection(PricingOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<ISubscriptionPricing, SubscriptionPricing>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISubscriptionService, Services.SubscriptionService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}
