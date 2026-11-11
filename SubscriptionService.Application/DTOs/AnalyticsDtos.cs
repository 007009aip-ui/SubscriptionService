// File: src/SubscriptionService.Application/DTOs/AnalyticsDtos.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.DTOs;

public sealed record ExpenseByTypeDto(SubscriptionType Type, string TypeName, decimal Amount, int PaymentsCount);

public sealed record ExpenseByMonthDto(int Year, int Month, string Period, decimal Total, IReadOnlyList<ExpenseByTypeDto> Breakdown);

/// <summary>GET /api/analytics/expenses response: totals + month/type breakdown (empty months are zero filled).</summary>
public sealed record ExpenseReportDto(
    DateOnly From,
    DateOnly To,
    int Months,
    string Currency,
    decimal Total,
    int PaymentsCount,
    IReadOnlyList<ExpenseByMonthDto> ByMonth,
    IReadOnlyList<ExpenseByTypeDto> ByType);
