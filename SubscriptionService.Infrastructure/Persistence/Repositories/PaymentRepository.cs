// File: src/SubscriptionService.Infrastructure/Persistence/Repositories/PaymentRepository.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Common;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Infrastructure.Persistence.Repositories;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _db;

    public PaymentRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default) =>
        await _db.Payments.AddAsync(payment, cancellationToken);

    public async Task<IReadOnlyList<SubscriptionExpense>> GetExpensesAsync(
        Guid userId, DateTime fromUtc, DateTime toUtcExclusive, CancellationToken cancellationToken = default) =>
        await _db.Payments
            .AsNoTracking()
            .Where(p => p.Subscription!.UserId == userId
                        && p.Status == PaymentStatus.Succeeded
                        && p.PaidAtUtc >= fromUtc
                        && p.PaidAtUtc < toUtcExclusive)
            .Select(p => new SubscriptionExpense(p.SubscriptionId, p.Subscription!.Type, p.Amount, p.PaidAtUtc))
            .ToListAsync(cancellationToken);
}
