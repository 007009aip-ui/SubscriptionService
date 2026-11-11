// File: src/SubscriptionService.Domain/Entities/Payment.cs
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Domain.Entities;

/// <summary>Money fact: one charge for one billing period of a subscription.</summary>
public class Payment
{
    /// <summary>Required by EF Core materialization.</summary>
    private Payment()
    {
    }

    public Guid Id { get; private set; }

    public Guid SubscriptionId { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; } = null!;

    public DateTime PeriodStart { get; private set; }

    public DateTime PeriodEnd { get; private set; }

    public DateTime PaidAtUtc { get; private set; }

    public PaymentStatus Status { get; private set; }

    public Subscription? Subscription { get; private set; }

    public static Payment Create(
        Guid subscriptionId,
        decimal amount,
        string currency,
        DateTime periodStart,
        DateTime periodEnd,
        DateTime paidAtUtc,
        PaymentStatus status = PaymentStatus.Succeeded)
    {
        if (subscriptionId == Guid.Empty)
        {
            throw new DomainException("SubscriptionId must not be empty.");
        }

        if (amount <= 0m || string.IsNullOrWhiteSpace(currency))
        {
            throw new DomainException("Payment amount must be positive and currency must be specified.");
        }

        if (periodEnd <= periodStart)
        {
            throw new DomainException("Payment period end must be after period start.");
        }

        return new Payment
        {
            Id = Guid.NewGuid(),
            SubscriptionId = subscriptionId,
            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero),
            Currency = currency,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            PaidAtUtc = paidAtUtc,
            Status = status
        };
    }
}
