// File: src/SubscriptionService.Application/Abstractions/Persistence/IPaymentRepository.cs
using SubscriptionService.Application.Common;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Abstractions.Persistence;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment, CancellationToken cancellationToken = default);

    /// <summary>Successful charges of the user inside [fromUtc, toUtcExclusive) - a flat read model for analytics.</summary>
    Task<IReadOnlyList<SubscriptionExpense>> GetExpensesAsync(
        Guid userId,
        DateTime fromUtc,
        DateTime toUtcExclusive,
        CancellationToken cancellationToken = default);
}
