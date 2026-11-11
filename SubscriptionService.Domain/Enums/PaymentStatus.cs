// File: src/SubscriptionService.Domain/Enums/PaymentStatus.cs
namespace SubscriptionService.Domain.Enums;

/// <summary>Outcome of a payment. Only <see cref="Succeeded"/> payments count as an expense.</summary>
public enum PaymentStatus
{
    Succeeded = 1,
    Failed = 2,
    Refunded = 3
}
