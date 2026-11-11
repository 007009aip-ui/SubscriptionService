// File: src/SubscriptionService.Application/Mappings/DtoMappings.cs
using SubscriptionService.Application.DTOs;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Mappings;

/// <summary>Explicit hand written mapping - no reflection based mapper, no hidden magic.</summary>
public static class DtoMappings
{
    public static UserDto ToDto(this User user) => new(user.Id, user.Email, user.Name);

    public static SubscriptionDto ToDto(this Subscription subscription, DateTime utcNow) => new(
        subscription.Id,
        subscription.Type,
        subscription.Type.ToString(),
        subscription.Status,
        subscription.Status.ToString(),
        subscription.StartDate,
        subscription.BillingPeriodInMonths,
        subscription.PricePerPeriod,
        subscription.Currency,
        subscription.CurrentPeriodEnd,
        subscription.CancelledAtUtc,
        subscription.HasAccessAt(utcNow));
}
