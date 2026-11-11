// File: src/SubscriptionService.Application/Services/SubscriptionService.cs
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Mappings;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Services;

/// <summary>Subscription use cases: list, create (transactionally with the first payment), cancel.</summary>
public sealed class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptions;
    private readonly IPaymentRepository _payments;
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionPricing _pricing;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public SubscriptionService(
        ISubscriptionRepository subscriptions,
        IPaymentRepository payments,
        IUserRepository users,
        IUnitOfWork unitOfWork,
        ISubscriptionPricing pricing,
        ICurrentUser currentUser,
        IClock clock)
    {
        _subscriptions = subscriptions;
        _payments = payments;
        _users = users;
        _unitOfWork = unitOfWork;
        _pricing = pricing;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<SubscriptionDto>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = _clock.UtcNow;
        var subscriptions = await _subscriptions.GetWithAccessByUserAsync(_currentUser.UserId, utcNow, cancellationToken);

        return subscriptions
            .OrderByDescending(s => s.StartDate)
            .Select(s => s.ToDto(utcNow))
            .ToList();
    }

    public async Task<SubscriptionDto> CreateAsync(CreateSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var utcNow = _clock.UtcNow;
        var userId = _currentUser.UserId;

        Validate(request, utcNow);

        _ = await _users.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), userId);

        if (await _subscriptions.HasRunningSubscriptionAsync(userId, utcNow, cancellationToken))
        {
            throw new ConflictException(
                "User already has an active subscription. Cancel it before creating a new one.");
        }

        var pricePerPeriod = _pricing.GetMonthlyPrice(request.Type) * request.BillingPeriodInMonths;

        var subscription = Subscription.Create(
            userId, request.Type, request.StartDate, request.BillingPeriodInMonths, pricePerPeriod, _pricing.Currency);

        var payment = subscription.CreateInitialPayment(utcNow);

        // Subscription + its first payment are written atomically: either both or nothing.
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _subscriptions.AddAsync(subscription, cancellationToken);
            await _payments.AddAsync(payment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return subscription.ToDto(utcNow);
    }

    public async Task<SubscriptionDto> CancelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var utcNow = _clock.UtcNow;
        var subscription = await _subscriptions.GetByIdAsync(id, cancellationToken);

        // Somebody else's subscription is reported as "not found" - do not leak existence of foreign ids.
        if (subscription is null || subscription.UserId != _currentUser.UserId)
        {
            throw new NotFoundException(nameof(Subscription), id);
        }

        if (subscription.Status == SubscriptionStatus.Cancelled)
        {
            throw new ConflictException("Subscription is already cancelled.");
        }

        if (!subscription.HasAccessAt(utcNow))
        {
            throw new ConflictException("Subscription period has already ended, there is nothing to cancel.");
        }

        subscription.Cancel(utcNow);

        // Single SaveChanges is already atomic - no explicit transaction needed here.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subscription.ToDto(utcNow);
    }

    private static void Validate(CreateSubscriptionRequest request, DateTime utcNow)
    {
        var errors = new Dictionary<string, string[]>();

        if (!Enum.IsDefined(request.Type))
        {
            errors[nameof(request.Type)] = new[] { "Subscription type must be Basic, Premium or Corporate." };
        }

        if (request.BillingPeriodInMonths is < Subscription.MinBillingPeriodInMonths or > Subscription.MaxBillingPeriodInMonths)
        {
            errors[nameof(request.BillingPeriodInMonths)] = new[]
            {
                $"Billing period must be between {Subscription.MinBillingPeriodInMonths} and {Subscription.MaxBillingPeriodInMonths} months."
            };
        }

        if (request.StartDate == default)
        {
            errors[nameof(request.StartDate)] = new[] { "StartDate is required." };
        }
        else if (request.StartDate.Date > utcNow.Date)
        {
            errors[nameof(request.StartDate)] = new[] { "StartDate cannot be in the future." };
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }
    }
}
