// File: src/SubscriptionService.Application/Services/AnalyticsService.cs
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.Common;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Services;

/// <summary>Expense analytics over successful payments, grouped by calendar month and plan.</summary>
public sealed class AnalyticsService : IAnalyticsService
{
    public const int DefaultMonths = 6;
    public const int MaxMonths = 36;

    private readonly IPaymentRepository _payments;
    private readonly ICurrentUser _currentUser;
    private readonly ISubscriptionPricing _pricing;
    private readonly IClock _clock;

    public AnalyticsService(
        IPaymentRepository payments,
        ICurrentUser currentUser,
        ISubscriptionPricing pricing,
        IClock clock)
    {
        _payments = payments;
        _currentUser = currentUser;
        _pricing = pricing;
        _clock = clock;
    }

    public async Task<ExpenseReportDto> GetExpensesAsync(int months, CancellationToken cancellationToken = default)
    {
        if (months is < 1 or > MaxMonths)
        {
            throw new ValidationException(nameof(months), $"Months must be between 1 and {MaxMonths}.");
        }

        var utcNow = _clock.UtcNow;
        var currentMonthStart = new DateTime(utcNow.Year, utcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var fromUtc = currentMonthStart.AddMonths(-(months - 1));
        var toUtcExclusive = currentMonthStart.AddMonths(1);

        var expenses = await _payments.GetExpensesAsync(_currentUser.UserId, fromUtc, toUtcExclusive, cancellationToken);

        var byMonth = new List<ExpenseByMonthDto>(months);
        for (var i = 0; i < months; i++)
        {
            var monthStart = fromUtc.AddMonths(i);
            var monthExpenses = expenses
                .Where(e => e.PaidAtUtc.Year == monthStart.Year && e.PaidAtUtc.Month == monthStart.Month)
                .ToList();

            byMonth.Add(new ExpenseByMonthDto(
                monthStart.Year,
                monthStart.Month,
                monthStart.ToString("yyyy-MM"),
                Sum(monthExpenses),
                GroupByType(monthExpenses)));
        }

        return new ExpenseReportDto(
            DateOnly.FromDateTime(fromUtc),
            DateOnly.FromDateTime(toUtcExclusive.AddDays(-1)),
            months,
            _pricing.Currency,
            Sum(expenses),
            expenses.Count,
            byMonth,
            GroupByType(expenses));
    }

    private static decimal Sum(IEnumerable<SubscriptionExpense> expenses) =>
        decimal.Round(expenses.Sum(e => e.Amount), 2, MidpointRounding.AwayFromZero);

    private static IReadOnlyList<ExpenseByTypeDto> GroupByType(IEnumerable<SubscriptionExpense> expenses) => expenses
        .GroupBy(e => e.Type)
        .OrderBy(g => g.Key)
        .Select(g => new ExpenseByTypeDto(g.Key, g.Key.ToString(), Sum(g), g.Count()))
        .ToList();
}
