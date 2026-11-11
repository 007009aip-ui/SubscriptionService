// File: src/SubscriptionService.Application/Abstractions/Services/IAnalyticsService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface IAnalyticsService
{
    Task<ExpenseReportDto> GetExpensesAsync(int months, CancellationToken cancellationToken = default);
}
