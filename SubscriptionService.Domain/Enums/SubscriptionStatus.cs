// File: src/SubscriptionService.Domain/Enums/SubscriptionStatus.cs
namespace SubscriptionService.Domain.Enums;

/// <summary>
/// Lifecycle status. "Expired" is not stored: it is derived from <c>CurrentPeriodEnd</c>,
/// so no background job is required to keep the data truthful.
/// </summary>
public enum SubscriptionStatus
{
    Active = 1,
    Cancelled = 2
}
