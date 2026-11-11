// File: src/SubscriptionService.Application/Abstractions/Persistence/ISubscriptionRepository.cs
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Abstractions.Persistence;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Subscriptions the user still has access to (active or cancelled but not yet expired).</summary>
    Task<IReadOnlyList<Subscription>> GetWithAccessByUserAsync(Guid userId, DateTime utcNow, CancellationToken cancellationToken = default);

    /// <summary>True when the user already has a running (Active and not expired) subscription.</summary>
    Task<bool> HasRunningSubscriptionAsync(Guid userId, DateTime utcNow, CancellationToken cancellationToken = default);

    Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
}
