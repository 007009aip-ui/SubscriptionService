// File: src/SubscriptionService.Domain/Entities/Subscription.cs
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Domain.Entities;

/// <summary>
/// Subscription aggregate root. Owns its paid period and the money related invariants.
/// Cancelling never shortens the paid period: access is kept until <see cref="CurrentPeriodEnd"/>.
/// </summary>
public class Subscription
{
    public const int MinBillingPeriodInMonths = 1;
    public const int MaxBillingPeriodInMonths = 12;

    private readonly List<Payment> _payments = new();

    /// <summary>Required by EF Core materialization.</summary>
    private Subscription()
    {
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public SubscriptionType Type { get; private set; }

    public SubscriptionStatus Status { get; private set; }

    public DateTime StartDate { get; private set; }

    public int BillingPeriodInMonths { get; private set; }

    /// <summary>Total price of one billing period (monthly price * months).</summary>
    public decimal PricePerPeriod { get; private set; }

    public string Currency { get; private set; } = null!;

    /// <summary>Exclusive upper bound of the paid period.</summary>
    public DateTime CurrentPeriodEnd { get; private set; }

    public DateTime? CancelledAtUtc { get; private set; }

    public User? User { get; private set; }

    public IReadOnlyCollection<Payment> Payments => _payments;

    /// <summary>Creates an active subscription for the first paid period.</summary>
    public static Subscription Create(
        Guid userId,
        SubscriptionType type,
        DateTime startDate,
        int billingPeriodInMonths,
        decimal pricePerPeriod,
        string currency)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException("UserId must not be empty.");
        }

        if (!Enum.IsDefined(type))
        {
            throw new DomainException($"Unknown subscription type '{type}'.");
        }

        if (billingPeriodInMonths is < MinBillingPeriodInMonths or > MaxBillingPeriodInMonths)
        {
            throw new DomainException(
                $"Billing period must be between {MinBillingPeriodInMonths} and {MaxBillingPeriodInMonths} months.");
        }

        if (startDate == default)
        {
            throw new DomainException("StartDate must be a valid date.");
        }

        if (pricePerPeriod <= 0m || string.IsNullOrWhiteSpace(currency))
        {
            throw new DomainException("Price must be positive and currency must be specified.");
        }

        var start = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Utc);

        return new Subscription
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = type,
            Status = SubscriptionStatus.Active,
            StartDate = start,
            BillingPeriodInMonths = billingPeriodInMonths,
            PricePerPeriod = decimal.Round(pricePerPeriod, 2, MidpointRounding.AwayFromZero),
            Currency = currency,
            CurrentPeriodEnd = start.AddMonths(billingPeriodInMonths)
        };
    }

    /// <summary>True while the paid period lasts - regardless of Active/Cancelled status.</summary>
    public bool HasAccessAt(DateTime utcNow) => utcNow < CurrentPeriodEnd;

    /// <summary>Payment for the very first period; must be persisted together with the subscription.</summary>
    public Payment CreateInitialPayment(DateTime paidAtUtc) =>
        Payment.Create(Id, PricePerPeriod, Currency, StartDate, CurrentPeriodEnd, paidAtUtc);

    /// <summary>Prolongs the paid period and returns the payment for it (used by renewal / seed).</summary>
    public Payment Renew(DateTime paidAtUtc)
    {
        if (Status != SubscriptionStatus.Active)
        {
            throw new DomainException("Only an active subscription can be renewed.");
        }

        var periodStart = CurrentPeriodEnd;
        CurrentPeriodEnd = periodStart.AddMonths(BillingPeriodInMonths);

        return Payment.Create(Id, PricePerPeriod, Currency, periodStart, CurrentPeriodEnd, paidAtUtc);
    }

    /// <summary>Stops the renewal. The user keeps access until the end of the paid period.</summary>
    public void Cancel(DateTime utcNow)
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            throw new DomainException("Subscription is already cancelled.");
        }

        if (!HasAccessAt(utcNow))
        {
            throw new DomainException("Subscription period has already ended.");
        }

        Status = SubscriptionStatus.Cancelled;
        CancelledAtUtc = utcNow;
    }
}
