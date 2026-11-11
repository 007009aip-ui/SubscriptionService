// File: src/SubscriptionService.Application/Abstractions/Services/ISubscriptionService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface ISubscriptionService
{
    Task<IReadOnlyList<SubscriptionDto>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<SubscriptionDto> CreateAsync(CreateSubscriptionRequest request, CancellationToken cancellationToken = default);

    Task<SubscriptionDto> CancelAsync(Guid id, CancellationToken cancellationToken = default);
}
