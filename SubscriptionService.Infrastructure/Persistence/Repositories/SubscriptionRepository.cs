// File: src/SubscriptionService.Infrastructure/Persistence/Repositories/SubscriptionRepository.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Infrastructure.Persistence.Repositories;

public sealed class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _db;

    public SubscriptionRepository(AppDbContext db) => _db = db;

    public Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Subscriptions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Subscription>> GetWithAccessByUserAsync(
        Guid userId, DateTime utcNow, CancellationToken cancellationToken = default) =>
        await _db.Subscriptions
            .AsNoTracking()
            .Where(s => s.UserId == userId && s.CurrentPeriodEnd > utcNow)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync(cancellationToken);

    public Task<bool> HasRunningSubscriptionAsync(
        Guid userId, DateTime utcNow, CancellationToken cancellationToken = default) =>
        _db.Subscriptions.AnyAsync(
            s => s.UserId == userId && s.Status == SubscriptionStatus.Active && s.CurrentPeriodEnd > utcNow,
            cancellationToken);

    public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default) =>
        await _db.Subscriptions.AddAsync(subscription, cancellationToken);
}
