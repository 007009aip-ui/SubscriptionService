# Subscription Service — полный исходный код
ASP.NET Core 8 · Clean Architecture · EF Core · JWT · xUnit + Moq + FluentAssertions.
Сборка: 0 warnings / 0 errors. Тесты: 103 passed. Покрытие сервисов: 100% statements / 100% branches.
Описание архитектуры, запуск и smoke-тесты — в `README.md`.

---
## 0. Solution и общие настройки сборки

### `SubscriptionService.sln`

```text
﻿
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "src", "src", "{75CF6968-6824-45EF-8D69-055291D52976}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "SubscriptionService.Domain", "src\SubscriptionService.Domain\SubscriptionService.Domain.csproj", "{B240F7E0-BCD9-413F-8B55-4F600A454A8F}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "SubscriptionService.Application", "src\SubscriptionService.Application\SubscriptionService.Application.csproj", "{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "SubscriptionService.Infrastructure", "src\SubscriptionService.Infrastructure\SubscriptionService.Infrastructure.csproj", "{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "SubscriptionService.Api", "src\SubscriptionService.Api\SubscriptionService.Api.csproj", "{9883C027-0BA7-4F5D-8496-1F85D32ED90F}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "tests", "tests", "{B184E3F0-603B-46E4-B19D-AC6D6DF14188}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "SubscriptionService.UnitTests", "tests\SubscriptionService.UnitTests\SubscriptionService.UnitTests.csproj", "{1684E245-9429-47D9-8263-54A1265CAAC8}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{B240F7E0-BCD9-413F-8B55-4F600A454A8F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B240F7E0-BCD9-413F-8B55-4F600A454A8F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B240F7E0-BCD9-413F-8B55-4F600A454A8F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B240F7E0-BCD9-413F-8B55-4F600A454A8F}.Release|Any CPU.Build.0 = Release|Any CPU
		{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA}.Release|Any CPU.Build.0 = Release|Any CPU
		{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC}.Release|Any CPU.Build.0 = Release|Any CPU
		{9883C027-0BA7-4F5D-8496-1F85D32ED90F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9883C027-0BA7-4F5D-8496-1F85D32ED90F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9883C027-0BA7-4F5D-8496-1F85D32ED90F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9883C027-0BA7-4F5D-8496-1F85D32ED90F}.Release|Any CPU.Build.0 = Release|Any CPU
		{1684E245-9429-47D9-8263-54A1265CAAC8}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1684E245-9429-47D9-8263-54A1265CAAC8}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1684E245-9429-47D9-8263-54A1265CAAC8}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1684E245-9429-47D9-8263-54A1265CAAC8}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{B240F7E0-BCD9-413F-8B55-4F600A454A8F} = {75CF6968-6824-45EF-8D69-055291D52976}
		{B3E7D3C6-6A7D-40EC-95D2-A87DE6A89FEA} = {75CF6968-6824-45EF-8D69-055291D52976}
		{2CFC17E3-04B0-4F76-9123-35E37BF7CBAC} = {75CF6968-6824-45EF-8D69-055291D52976}
		{9883C027-0BA7-4F5D-8496-1F85D32ED90F} = {75CF6968-6824-45EF-8D69-055291D52976}
		{1684E245-9429-47D9-8263-54A1265CAAC8} = {B184E3F0-603B-46E4-B19D-AC6D6DF14188}
	EndGlobalSection
EndGlobal
```

### `Directory.Build.props`

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>12</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
    <EnableNETAnalyzers>true</EnableNETAnalyzers>
    <AnalysisLevel>latest</AnalysisLevel>
  </PropertyGroup>
</Project>
```

### `src/SubscriptionService.Api/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Issuer": "SubscriptionService",
    "Audience": "SubscriptionService.Clients",
    "SigningKey": "dev-only-super-secret-signing-key-change-me-in-production-0123456789",
    "ExpirationMinutes": 60
  },
  "Database": {
    "Provider": "SqliteInMemory",
    "ConnectionString": "",
    "SeedDemoData": true
  },
  "Pricing": {
    "BasicMonthlyPrice": 10,
    "PremiumMonthlyPrice": 25,
    "CorporateMonthlyPrice": 100,
    "Currency": "USD"
  }
}
```

### `src/SubscriptionService.Api/appsettings.Development.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
}
```

---

## Шаг 1. Domain — сущности и бизнес-инварианты

### `src/SubscriptionService.Domain/Entities/Payment.cs`

```csharp
// File: src/SubscriptionService.Domain/Entities/Payment.cs
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Domain.Entities;

/// <summary>Money fact: one charge for one billing period of a subscription.</summary>
public class Payment
{
    /// <summary>Required by EF Core materialization.</summary>
    private Payment()
    {
    }

    public Guid Id { get; private set; }

    public Guid SubscriptionId { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; } = null!;

    public DateTime PeriodStart { get; private set; }

    public DateTime PeriodEnd { get; private set; }

    public DateTime PaidAtUtc { get; private set; }

    public PaymentStatus Status { get; private set; }

    public Subscription? Subscription { get; private set; }

    public static Payment Create(
        Guid subscriptionId,
        decimal amount,
        string currency,
        DateTime periodStart,
        DateTime periodEnd,
        DateTime paidAtUtc,
        PaymentStatus status = PaymentStatus.Succeeded)
    {
        if (subscriptionId == Guid.Empty)
        {
            throw new DomainException("SubscriptionId must not be empty.");
        }

        if (amount <= 0m || string.IsNullOrWhiteSpace(currency))
        {
            throw new DomainException("Payment amount must be positive and currency must be specified.");
        }

        if (periodEnd <= periodStart)
        {
            throw new DomainException("Payment period end must be after period start.");
        }

        return new Payment
        {
            Id = Guid.NewGuid(),
            SubscriptionId = subscriptionId,
            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero),
            Currency = currency,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            PaidAtUtc = paidAtUtc,
            Status = status
        };
    }
}
```

### `src/SubscriptionService.Domain/Entities/Subscription.cs`

```csharp
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
```

### `src/SubscriptionService.Domain/Entities/User.cs`

```csharp
// File: src/SubscriptionService.Domain/Entities/User.cs
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Domain.Entities;

/// <summary>Application user. Password is never stored in plain text - only the hash.</summary>
public class User
{
    private readonly List<Subscription> _subscriptions = new();

    /// <summary>Required by EF Core materialization.</summary>
    private User()
    {
    }

    public User(Guid id, string email, string passwordHash, string name, DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("User id must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Email, password hash and name are required.");
        }

        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; private set; }

    /// <summary>Normalized (trimmed, lower-cased) e-mail, unique across the system.</summary>
    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; }

    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;
}
```

### `src/SubscriptionService.Domain/Enums/PaymentStatus.cs`

```csharp
// File: src/SubscriptionService.Domain/Enums/PaymentStatus.cs
namespace SubscriptionService.Domain.Enums;

/// <summary>Outcome of a payment. Only <see cref="Succeeded"/> payments count as an expense.</summary>
public enum PaymentStatus
{
    Succeeded = 1,
    Failed = 2,
    Refunded = 3
}
```

### `src/SubscriptionService.Domain/Enums/SubscriptionStatus.cs`

```csharp
// File: src/SubscriptionService.Domain/Enums/SubscriptionStatus.cs
namespace SubscriptionService.Domain.Enums;

/// <summary>
/// Lifecycle status. "Expired" is not stored: it is derived from <c>CurrentPeriodEnd</c>,
/// so no background job is required to keep the data truthful.
/// </summary>
public enum SubscriptionStatus
{
    Active = 1,
    Cancelled = 2
}
```

### `src/SubscriptionService.Domain/Enums/SubscriptionType.cs`

```csharp
// File: src/SubscriptionService.Domain/Enums/SubscriptionType.cs
namespace SubscriptionService.Domain.Enums;

/// <summary>Tariff plan of a subscription.</summary>
public enum SubscriptionType
{
    Basic = 1,
    Premium = 2,
    Corporate = 3
}
```

### `src/SubscriptionService.Domain/Exceptions/AppExceptions.cs`

```csharp
// File: src/SubscriptionService.Domain/Exceptions/AppExceptions.cs
namespace SubscriptionService.Domain.Exceptions;

/// <summary>Base class for expected (business) failures. Mapped to HTTP codes by the API middleware.</summary>
public abstract class AppException : Exception
{
    protected AppException(string message) : base(message)
    {
    }

    /// <summary>Stable machine readable error code returned in ProblemDetails.</summary>
    public abstract string Code { get; }
}

/// <summary>Invalid input -> HTTP 400.</summary>
public sealed class ValidationException : AppException
{
    public ValidationException(string field, string error)
        : this(new Dictionary<string, string[]> { [field] = new[] { error } })
    {
    }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public override string Code => "validation_error";
}

/// <summary>Missing resource (or a resource owned by somebody else) -> HTTP 404.</summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string entity, object key)
        : base($"{entity} with key '{key}' was not found.")
    {
    }

    public override string Code => "not_found";
}

/// <summary>Business rule conflict -> HTTP 409.</summary>
public sealed class ConflictException : AppException
{
    public ConflictException(string message) : base(message)
    {
    }

    public override string Code => "conflict";
}

/// <summary>Failed authentication -> HTTP 401.</summary>
public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override string Code => "unauthorized";
}

/// <summary>Violation of a domain invariant (guard clause inside an entity).</summary>
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
```

### `src/SubscriptionService.Domain/SubscriptionService.Domain.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <!-- Domain: entities + business rules. No external dependencies (persistence-ignorant). -->
</Project>
```

---

## Шаги 2–3. Application — DTO, порты, сервисы (бизнес-логика)

### `src/SubscriptionService.Application/Abstractions/IClock.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/IClock.cs
namespace SubscriptionService.Application.Abstractions;

/// <summary>Time is an external dependency - injecting it makes time dependent logic testable.</summary>
public interface IClock
{
    DateTime UtcNow { get; }
}
```

### `src/SubscriptionService.Application/Abstractions/ICurrentUser.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/ICurrentUser.cs
namespace SubscriptionService.Application.Abstractions;

/// <summary>Ambient user of the current request, resolved from the JWT by the API layer.</summary>
public interface ICurrentUser
{
    Guid UserId { get; }
}
```

### `src/SubscriptionService.Application/Abstractions/IJwtTokenGenerator.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/IJwtTokenGenerator.cs
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Abstractions;

public sealed record JwtToken(string AccessToken, DateTime ExpiresAtUtc);

public interface IJwtTokenGenerator
{
    JwtToken Generate(User user);
}
```

### `src/SubscriptionService.Application/Abstractions/IPasswordHasher.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/IPasswordHasher.cs
namespace SubscriptionService.Application.Abstractions;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string passwordHash, string password);
}
```

### `src/SubscriptionService.Application/Abstractions/Persistence/IPaymentRepository.cs`

```csharp
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
```

### `src/SubscriptionService.Application/Abstractions/Persistence/ISubscriptionRepository.cs`

```csharp
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
```

### `src/SubscriptionService.Application/Abstractions/Persistence/IUnitOfWork.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/Persistence/IUnitOfWork.cs
namespace SubscriptionService.Application.Abstractions.Persistence;

/// <summary>Atomicity boundary. Implemented by EF Core; mocked in unit tests.</summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IAppTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}

/// <summary>Provider agnostic transaction (no-op for non relational providers such as EF InMemory).</summary>
public interface IAppTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
```

### `src/SubscriptionService.Application/Abstractions/Persistence/IUserRepository.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/Persistence/IUserRepository.cs
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
}
```

### `src/SubscriptionService.Application/Abstractions/Services/IAnalyticsService.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/Services/IAnalyticsService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface IAnalyticsService
{
    Task<ExpenseReportDto> GetExpensesAsync(int months, CancellationToken cancellationToken = default);
}
```

### `src/SubscriptionService.Application/Abstractions/Services/IAuthService.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/Services/IAuthService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
```

### `src/SubscriptionService.Application/Abstractions/Services/ISubscriptionService.cs`

```csharp
// File: src/SubscriptionService.Application/Abstractions/Services/ISubscriptionService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface ISubscriptionService
{
    Task<IReadOnlyList<SubscriptionDto>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<SubscriptionDto> CreateAsync(CreateSubscriptionRequest request, CancellationToken cancellationToken = default);

    Task<SubscriptionDto> CancelAsync(Guid id, CancellationToken cancellationToken = default);
}
```

### `src/SubscriptionService.Application/Common/SubscriptionExpense.cs`

```csharp
// File: src/SubscriptionService.Application/Common/SubscriptionExpense.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.Common;

/// <summary>Denormalized payment row used by the analytics service (EF translates the projection to SQL).</summary>
public sealed record SubscriptionExpense(Guid SubscriptionId, SubscriptionType Type, decimal Amount, DateTime PaidAtUtc);
```

### `src/SubscriptionService.Application/DTOs/AnalyticsDtos.cs`

```csharp
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
```

### `src/SubscriptionService.Application/DTOs/AuthDtos.cs`

```csharp
// File: src/SubscriptionService.Application/DTOs/AuthDtos.cs
namespace SubscriptionService.Application.DTOs;

/// <summary>POST /api/auth/register payload.</summary>
public sealed record RegisterRequest(string Email, string Password, string Name);

/// <summary>POST /api/auth/login payload.</summary>
public sealed record LoginRequest(string Email, string Password);

public sealed record UserDto(Guid Id, string Email, string Name);

/// <summary>Issued JWT + its lifetime + the user profile.</summary>
public sealed record AuthResponse(string AccessToken, string TokenType, DateTime ExpiresAtUtc, UserDto User);
```

### `src/SubscriptionService.Application/DTOs/SubscriptionDtos.cs`

```csharp
// File: src/SubscriptionService.Application/DTOs/SubscriptionDtos.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.DTOs;

/// <summary>POST /api/subscriptions payload. Type accepts "Basic" | "Premium" | "Corporate" (or 1 | 2 | 3).</summary>
public sealed record CreateSubscriptionRequest(SubscriptionType Type, DateTime StartDate, int BillingPeriodInMonths);

public sealed record SubscriptionDto(
    Guid Id,
    SubscriptionType Type,
    string TypeName,
    SubscriptionStatus Status,
    string StatusName,
    DateTime StartDate,
    int BillingPeriodInMonths,
    decimal PricePerPeriod,
    string Currency,
    DateTime CurrentPeriodEnd,
    DateTime? CancelledAtUtc,
    bool HasAccess);
```

### `src/SubscriptionService.Application/DependencyInjection.cs`

```csharp
// File: src/SubscriptionService.Application/DependencyInjection.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.Options;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Application.Services;

namespace SubscriptionService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PricingOptions>()
            .Bind(configuration.GetSection(PricingOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<ISubscriptionPricing, SubscriptionPricing>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISubscriptionService, Services.SubscriptionService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}
```

### `src/SubscriptionService.Application/Mappings/DtoMappings.cs`

```csharp
// File: src/SubscriptionService.Application/Mappings/DtoMappings.cs
using SubscriptionService.Application.DTOs;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Mappings;

/// <summary>Explicit hand written mapping - no reflection based mapper, no hidden magic.</summary>
public static class DtoMappings
{
    public static UserDto ToDto(this User user) => new(user.Id, user.Email, user.Name);

    public static SubscriptionDto ToDto(this Subscription subscription, DateTime utcNow) => new(
        subscription.Id,
        subscription.Type,
        subscription.Type.ToString(),
        subscription.Status,
        subscription.Status.ToString(),
        subscription.StartDate,
        subscription.BillingPeriodInMonths,
        subscription.PricePerPeriod,
        subscription.Currency,
        subscription.CurrentPeriodEnd,
        subscription.CancelledAtUtc,
        subscription.HasAccessAt(utcNow));
}
```

### `src/SubscriptionService.Application/Options/JwtOptions.cs`

```csharp
// File: src/SubscriptionService.Application/Options/JwtOptions.cs
using System.ComponentModel.DataAnnotations;

namespace SubscriptionService.Application.Options;

/// <summary>JWT settings. Bound from configuration section "Jwt" and validated on startup.</summary>
public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required]
    public string Issuer { get; set; } = null!;

    [Required]
    public string Audience { get; set; } = null!;

    /// <summary>HS256 requires at least 256 bits (32 ASCII characters). Use user-secrets / env vars in production.</summary>
    [Required]
    [MinLength(32)]
    public string SigningKey { get; set; } = null!;

    [Range(1, 1440)]
    public int ExpirationMinutes { get; set; } = 60;
}
```

### `src/SubscriptionService.Application/Options/PricingOptions.cs`

```csharp
// File: src/SubscriptionService.Application/Options/PricingOptions.cs
namespace SubscriptionService.Application.Options;

/// <summary>Monthly prices per plan. Bound from configuration section "Pricing".</summary>
public sealed class PricingOptions
{
    public const string SectionName = "Pricing";

    public decimal BasicMonthlyPrice { get; set; } = 10m;

    public decimal PremiumMonthlyPrice { get; set; } = 25m;

    public decimal CorporateMonthlyPrice { get; set; } = 100m;

    public string Currency { get; set; } = "USD";
}
```

### `src/SubscriptionService.Application/Pricing/ISubscriptionPricing.cs`

```csharp
// File: src/SubscriptionService.Application/Pricing/ISubscriptionPricing.cs
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Application.Pricing;

/// <summary>Single source of truth for prices (Open/Closed: a new plan does not touch the service logic).</summary>
public interface ISubscriptionPricing
{
    string Currency { get; }

    decimal GetMonthlyPrice(SubscriptionType type);
}
```

### `src/SubscriptionService.Application/Pricing/SubscriptionPricing.cs`

```csharp
// File: src/SubscriptionService.Application/Pricing/SubscriptionPricing.cs
using Microsoft.Extensions.Options;
using SubscriptionService.Application.Options;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Pricing;

public sealed class SubscriptionPricing : ISubscriptionPricing
{
    private readonly PricingOptions _options;

    public SubscriptionPricing(IOptions<PricingOptions> options) => _options = options.Value;

    public string Currency => _options.Currency;

    public decimal GetMonthlyPrice(SubscriptionType type) => type switch
    {
        SubscriptionType.Basic => _options.BasicMonthlyPrice,
        SubscriptionType.Premium => _options.PremiumMonthlyPrice,
        SubscriptionType.Corporate => _options.CorporateMonthlyPrice,
        _ => throw new ValidationException(nameof(SubscriptionType), $"Unknown subscription type '{type}'.")
    };
}
```

### `src/SubscriptionService.Application/Services/AnalyticsService.cs`

```csharp
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
```

### `src/SubscriptionService.Application/Services/AuthService.cs`

```csharp
// File: src/SubscriptionService.Application/Services/AuthService.cs
using System.Text.RegularExpressions;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Mappings;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Services;

public sealed class AuthService : IAuthService
{
    private const int MinPasswordLength = 8;
    private const int MaxPasswordLength = 128;
    private const int MinNameLength = 2;
    private const int MaxNameLength = 100;

    private static readonly Regex EmailPattern = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IClock _clock;

    public AuthService(
        IUserRepository users,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator,
        IClock clock)
    {
        _users = users;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _clock = clock;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var email = NormalizeEmail(request.Email);
        var name = request.Name?.Trim() ?? string.Empty;
        var errors = new Dictionary<string, string[]>();

        if (!EmailPattern.IsMatch(email))
        {
            errors[nameof(request.Email)] = new[] { "A valid e-mail address is required." };
        }

        var passwordErrors = ValidatePassword(request.Password);
        if (passwordErrors.Length > 0)
        {
            errors[nameof(request.Password)] = passwordErrors;
        }

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors[nameof(request.Name)] = new[] { $"Name must be between {MinNameLength} and {MaxNameLength} characters." };
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        if (await _users.ExistsByEmailAsync(email, cancellationToken))
        {
            throw new ConflictException($"User with e-mail '{email}' is already registered.");
        }

        var user = new User(Guid.NewGuid(), email, _passwordHasher.Hash(request.Password), name, _clock.UtcNow);

        await _users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return BuildAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var email = NormalizeEmail(request.Email);
        if (email.Length == 0 || string.IsNullOrEmpty(request.Password))
        {
            throw new ValidationException(nameof(request.Email), "E-mail and password are required.");
        }

        var user = await _users.GetByEmailAsync(email, cancellationToken);

        // Same message for unknown e-mail and wrong password: do not leak which accounts exist.
        if (user is null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedException("Invalid e-mail or password.");
        }

        return BuildAuthResponse(user);
    }

    private static string NormalizeEmail(string? email) => email?.Trim().ToLowerInvariant() ?? string.Empty;

    private static string[] ValidatePassword(string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return new[] { "Password is required." };
        }

        var errors = new List<string>();
        if (password.Length is < MinPasswordLength or > MaxPasswordLength)
        {
            errors.Add($"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters.");
        }

        if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
        {
            errors.Add("Password must contain at least one letter and one digit.");
        }

        return errors.ToArray();
    }

    private AuthResponse BuildAuthResponse(User user)
    {
        var token = _tokenGenerator.Generate(user);
        return new AuthResponse(token.AccessToken, "Bearer", token.ExpiresAtUtc, user.ToDto());
    }
}
```

### `src/SubscriptionService.Application/Services/SubscriptionService.cs`

```csharp
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
```

### `src/SubscriptionService.Application/SubscriptionService.Application.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="8.0.2" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Options.DataAnnotations" Version="8.0.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\SubscriptionService.Domain\SubscriptionService.Domain.csproj" />
  </ItemGroup>
</Project>
```

---

## Шаги 1, 5. Infrastructure — DbContext, репозитории, JWT, хеширование, DI

### `src/SubscriptionService.Infrastructure/Common/SystemClock.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Common/SystemClock.cs
using SubscriptionService.Application.Abstractions;

namespace SubscriptionService.Infrastructure.Common;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
```

### `src/SubscriptionService.Infrastructure/DatabaseOptions.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/DatabaseOptions.cs
namespace SubscriptionService.Infrastructure;

public enum DatabaseProvider
{
    /// <summary>Default: real relational SQL + transactions, zero setup, data lives while the process runs.</summary>
    SqliteInMemory,

    /// <summary>EF InMemory provider (no transactions, no SQL).</summary>
    InMemory,

    /// <summary>Production provider - only a connection string change is required.</summary>
    Postgres
}

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public string Provider { get; set; } = nameof(DatabaseProvider.SqliteInMemory);

    public string? ConnectionString { get; set; }

    public bool SeedDemoData { get; set; } = true;
}
```

### `src/SubscriptionService.Infrastructure/DependencyInjection.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/DependencyInjection.cs
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Infrastructure.Common;
using SubscriptionService.Infrastructure.Persistence;
using SubscriptionService.Infrastructure.Persistence.Repositories;
using SubscriptionService.Infrastructure.Security;

namespace SubscriptionService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        if (!Enum.TryParse<DatabaseProvider>(options.Provider, ignoreCase: true, out var provider))
        {
            throw new InvalidOperationException(
                $"Unknown database provider '{options.Provider}'. Allowed: {string.Join(", ", Enum.GetNames<DatabaseProvider>())}.");
        }

        switch (provider)
        {
            case DatabaseProvider.SqliteInMemory:
                // Keep one open connection for the whole process: closing it would drop the in-memory database.
                services.AddSingleton(_ =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    return connection;
                });
                services.AddDbContext<AppDbContext>((sp, o) =>
                    o.UseSqlite(sp.GetRequiredService<SqliteConnection>()));
                break;

            case DatabaseProvider.InMemory:
                services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("SubscriptionService"));
                break;

            case DatabaseProvider.Postgres:
                if (string.IsNullOrWhiteSpace(options.ConnectionString))
                {
                    throw new InvalidOperationException("Database:ConnectionString is required for the Postgres provider.");
                }

                services.AddDbContext<AppDbContext>(o => o.UseNpgsql(options.ConnectionString));
                break;
        }

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IPasswordHasher, IdentityPasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/AppDbContext.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Infrastructure.Persistence.Configurations;

namespace SubscriptionService.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // SQLite/InMemory return DateTimeKind.Unspecified; normalize to UTC so the same model
    // works with Npgsql 'timestamp with time column' without any change.
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
        configurationBuilder.Properties<DateTime?>().HaveConversion<NullableUtcDateTimeConverter>();
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.Property(p => p.Currency).HasMaxLength(3).IsRequired();
        builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(p => p.PaidAtUtc).IsRequired();
        builder.HasIndex(p => p.SubscriptionId);
        builder.HasIndex(p => p.PaidAtUtc);
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/Configurations/SubscriptionConfiguration.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/SubscriptionConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

public sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Type).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(s => s.PricePerPeriod).HasPrecision(18, 2);
        builder.Property(s => s.Currency).HasMaxLength(3).IsRequired();
        builder.Property(s => s.StartDate).IsRequired();
        builder.Property(s => s.CurrentPeriodEnd).IsRequired();
        builder.HasIndex(s => new { s.UserId, s.Status });
        builder.HasIndex(s => s.CurrentPeriodEnd);

        builder.HasMany(s => s.Payments)
            .WithOne(p => p.Subscription)
            .HasForeignKey(p => p.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(s => s.Payments).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/Configurations/UserConfiguration.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/UserConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.PasswordHash).HasMaxLength(512).IsRequired();
        builder.Property(u => u.Name).HasMaxLength(100).IsRequired();
        builder.Property(u => u.CreatedAtUtc).IsRequired();

        builder.HasMany(u => u.Subscriptions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(u => u.Subscriptions).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/Configurations/UtcDateTimeConverter.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/Configurations/UtcDateTimeConverter.cs
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SubscriptionService.Infrastructure.Persistence.Configurations;

/// <summary>Guarantees that every DateTime leaving/entering the database is DateTimeKind.Utc.</summary>
public sealed class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter()
        : base(value => DateTime.SpecifyKind(value, DateTimeKind.Utc),
               value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
    {
    }
}

public sealed class NullableUtcDateTimeConverter : ValueConverter<DateTime?, DateTime?>
{
    public NullableUtcDateTimeConverter()
        : base(value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value,
               value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value)
    {
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/DbInitializer.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/DbInitializer.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.Infrastructure.Persistence;

/// <summary>Creates the schema (demo mode) and seeds one demo user with a payment history.</summary>
public static class DbInitializer
{
    public const string DemoEmail = "demo@subscriptions.local";
    public const string DemoPassword = "Demo1234!";

    public static async Task InitializeAsync(
        AppDbContext db,
        IPasswordHasher passwordHasher,
        IClock clock,
        CancellationToken cancellationToken = default)
    {
        // Demo/dev shortcut. For PostgreSQL production use 'dotnet ef migrations add' + Database.Migrate().
        await db.Database.EnsureCreatedAsync(cancellationToken);

        if (await db.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var utcNow = clock.UtcNow;
        var monthStart = new DateTime(utcNow.Year, utcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var user = new User(Guid.NewGuid(), DemoEmail, passwordHasher.Hash(DemoPassword), "Demo User", utcNow);
        db.Users.Add(user);

        // Basic: started 5 months ago, renewed twice, cancelled 3 months ago (access ended 2 months ago).
        var basic = Subscription.Create(user.Id, SubscriptionType.Basic, monthStart.AddMonths(-5), 1, 10m, "USD");
        var payments = new List<Payment>
        {
            basic.CreateInitialPayment(monthStart.AddMonths(-5)),
            basic.Renew(monthStart.AddMonths(-4)),
            basic.Renew(monthStart.AddMonths(-3))
        };
        basic.Cancel(monthStart.AddMonths(-3));
        db.Subscriptions.Add(basic);

        // Premium: started 2 months ago, still active, paid for the current month.
        var premium = Subscription.Create(user.Id, SubscriptionType.Premium, monthStart.AddMonths(-2), 1, 25m, "USD");
        payments.Add(premium.CreateInitialPayment(monthStart.AddMonths(-2)));
        payments.Add(premium.Renew(monthStart.AddMonths(-1)));
        payments.Add(premium.Renew(monthStart));
        db.Subscriptions.Add(premium);

        db.Payments.AddRange(payments);
        await db.SaveChangesAsync(cancellationToken);
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/EfUnitOfWork.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/EfUnitOfWork.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SubscriptionService.Application.Abstractions.Persistence;

namespace SubscriptionService.Infrastructure.Persistence;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    public EfUnitOfWork(AppDbContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);

    public async Task<IAppTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        // EF InMemory provider has no transaction support - degrade gracefully instead of throwing.
        if (!_db.Database.IsRelational())
        {
            return new NoOpTransaction();
        }

        var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        return new EfTransaction(transaction);
    }

    private sealed class EfTransaction : IAppTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EfTransaction(IDbContextTransaction transaction) => _transaction = transaction;

        public Task CommitAsync(CancellationToken cancellationToken = default) => _transaction.CommitAsync(cancellationToken);

        public Task RollbackAsync(CancellationToken cancellationToken = default) => _transaction.RollbackAsync(cancellationToken);

        public ValueTask DisposeAsync() => _transaction.DisposeAsync();
    }

    private sealed class NoOpTransaction : IAppTransaction
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task RollbackAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
```

### `src/SubscriptionService.Infrastructure/Persistence/Repositories/PaymentRepository.cs`

```csharp
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
```

### `src/SubscriptionService.Infrastructure/Persistence/Repositories/SubscriptionRepository.cs`

```csharp
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
```

### `src/SubscriptionService.Infrastructure/Persistence/Repositories/UserRepository.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Persistence/Repositories/UserRepository.cs
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default) =>
        _db.Users.FirstOrDefaultAsync(u => u.Email == normalizedEmail, cancellationToken);

    public Task<bool> ExistsByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default) =>
        _db.Users.AnyAsync(u => u.Email == normalizedEmail, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await _db.Users.AddAsync(user, cancellationToken);
}
```

### `src/SubscriptionService.Infrastructure/Security/IdentityPasswordHasher.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Security/IdentityPasswordHasher.cs
using Microsoft.AspNetCore.Identity;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Security;

/// <summary>
/// PBKDF2 (HMAC-SHA256, 100k iterations, per-password salt) via ASP.NET Core Identity's PasswordHasher.
/// The user argument is only used by custom hashers, the default implementation ignores it.
/// </summary>
public sealed class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(null!, password);

    public bool Verify(string passwordHash, string password)
    {
        try
        {
            var result = _hasher.VerifyHashedPassword(null!, passwordHash, password);
            return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
```

### `src/SubscriptionService.Infrastructure/Security/JwtTokenGenerator.cs`

```csharp
// File: src/SubscriptionService.Infrastructure/Security/JwtTokenGenerator.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Options;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Security;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly IClock _clock;

    public JwtTokenGenerator(IOptions<JwtOptions> options, IClock clock)
    {
        _options = options.Value;
        _clock = clock;
    }

    public JwtToken Generate(User user)
    {
        var issuedAt = _clock.UtcNow;
        var expiresAt = issuedAt.AddMinutes(_options.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("name", user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: issuedAt,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
```

### `src/SubscriptionService.Infrastructure/SubscriptionService.Infrastructure.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.11" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.11" />
    <PackageReference Include="Microsoft.Extensions.Identity.Core" Version="8.0.11" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.6.2" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="8.0.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\SubscriptionService.Application\SubscriptionService.Application.csproj" />
  </ItemGroup>
</Project>
```

---

## Шаги 4–5. Api — контроллеры, middleware, Program.cs, конфиг

### `src/SubscriptionService.Api/Controllers/AnalyticsController.cs`

```csharp
// File: src/SubscriptionService.Api/Controllers/AnalyticsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Services;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/analytics")]
[Produces("application/json")]
public sealed class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService) => _analyticsService = analyticsService;

    /// <summary>Subscription expenses for the last N calendar months, grouped by month and plan.</summary>
    /// <param name="months">Number of months including the current one (1-36, default 6).</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("expenses")]
    [ProducesResponseType(typeof(ExpenseReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseReportDto>> GetExpenses(
        [FromQuery] int months = AnalyticsService.DefaultMonths,
        CancellationToken cancellationToken = default)
        => Ok(await _analyticsService.GetExpensesAsync(months, cancellationToken));
}
```

### `src/SubscriptionService.Api/Controllers/AuthController.cs`

```csharp
// File: src/SubscriptionService.Api/Controllers/AuthController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    /// <summary>Registers a user and returns a JWT access token.</summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        => Ok(await _authService.RegisterAsync(request, cancellationToken));

    /// <summary>Authenticates a user and returns a JWT access token.</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        => Ok(await _authService.LoginAsync(request, cancellationToken));
}
```

### `src/SubscriptionService.Api/Controllers/SubscriptionsController.cs`

```csharp
// File: src/SubscriptionService.Api/Controllers/SubscriptionsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/subscriptions")]
[Produces("application/json")]
public sealed class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService) => _subscriptionService = subscriptionService;

    /// <summary>Subscriptions of the current user that still grant access (including cancelled but paid ones).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SubscriptionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<SubscriptionDto>>> GetActive(CancellationToken cancellationToken)
        => Ok(await _subscriptionService.GetActiveAsync(cancellationToken));

    /// <summary>Creates a subscription and charges the first billing period (single transaction).</summary>
    [HttpPost]
    [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SubscriptionDto>> Create([FromBody] CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionService.CreateAsync(request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, subscription);
    }

    /// <summary>Cancels the subscription; access is preserved until the end of the paid period.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SubscriptionDto>> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
        => Ok(await _subscriptionService.CancelAsync(id, cancellationToken));
}
```

### `src/SubscriptionService.Api/Middleware/ExceptionHandlingMiddleware.cs`

```csharp
// File: src/SubscriptionService.Api/Middleware/ExceptionHandlingMiddleware.cs
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Api.Middleware;

/// <summary>Translates business exceptions into RFC 7807 ProblemDetails; keeps controllers free of try/catch.</summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogError(exception, "Response already started, cannot write ProblemDetails.");
                throw;
            }

            await WriteProblemDetailsAsync(context, exception);
        }
    }

    private async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, code) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation failed", "validation_error"),
            DomainException => (StatusCodes.Status400BadRequest, "Domain rule violation", "domain_rule_violation"),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized", "unauthorized"),
            NotFoundException => (StatusCodes.Status404NotFound, "Resource not found", "not_found"),
            ConflictException => (StatusCodes.Status409Conflict, "Conflict", "conflict"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error", "internal_error")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
        }
        else
        {
            _logger.LogWarning("Request {Method} {Path} failed: {Message}", context.Request.Method, context.Request.Path, exception.Message);
        }

        var isExpected = exception is AppException or DomainException;

        ProblemDetails problem = exception is ValidationException validation
            ? new HttpValidationProblemDetails(validation.Errors.ToDictionary(e => e.Key, e => e.Value))
            {
                Status = statusCode,
                Title = title,
                Detail = validation.Message
            }
            : new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = isExpected ? exception.Message : "An unexpected error occurred. Please contact support."
            };

        problem.Extensions["code"] = code;
        problem.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        await JsonSerializer.SerializeAsync(context.Response.Body, problem, problem.GetType());
    }
}
```

### `src/SubscriptionService.Api/Program.cs`

```csharp
// File: src/SubscriptionService.Api/Program.cs
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SubscriptionService.Api.Middleware;
using SubscriptionService.Api.Services;
using SubscriptionService.Application;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Options;
using SubscriptionService.Infrastructure;
using SubscriptionService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ---- Layers -------------------------------------------------------------
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, HttpCurrentUser>();

// ---- MVC + JSON (enums as strings: "Premium", "Active", ...) ------------
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Model binding errors (malformed JSON, unknown enum name) use the same ProblemDetails contract
// as business validation errors coming from the Application layer.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(entry => entry.Key, entry => entry.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        var problem = new HttpValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation failed",
            Detail = "Request payload is invalid."
        };
        problem.Extensions["code"] = "validation_error";
        problem.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

        return new BadRequestObjectResult(problem) { ContentTypes = { "application/problem+json" } };
    };
});

// ---- Authentication / Authorization ------------------------------------
var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
          ?? throw new InvalidOperationException("Configuration section 'Jwt' is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false; // keep original JWT claim names ("sub", "email", ...)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey)),
            NameClaimType = "sub"
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Defence in depth: every endpoint requires a valid token unless it is explicitly [AllowAnonymous].
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

// ---- Swagger / OpenAPI -------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Subscription Service API",
        Version = "v1",
        Description = "User subscriptions, payments and expense analytics. Authorize with the JWT from /api/auth/login."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Paste the access token (without the 'Bearer ' prefix)."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "SubscriptionService.Api.xml");
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddHealthChecks();

var app = builder.Build();

// ---- Pipeline ----------------------------------------------------------
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Demo-friendly: Swagger is always on. Restrict it to Development behind a gateway in production.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Subscription Service API v1");
    options.DocumentTitle = "Subscription Service API";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();
app.MapGet("/", () => Results.Redirect("/swagger")).AllowAnonymous();

// ---- Schema + demo seed ------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var db = provider.GetRequiredService<AppDbContext>();
    var dbOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

    if (dbOptions.SeedDemoData)
    {
        await DbInitializer.InitializeAsync(
            db, provider.GetRequiredService<IPasswordHasher>(), provider.GetRequiredService<IClock>());
    }
    else
    {
        await db.Database.EnsureCreatedAsync();
    }
}

await app.RunAsync();

// Makes WebApplicationFactory<Program> possible for integration tests.
public partial class Program;
```

### `src/SubscriptionService.Api/Services/HttpCurrentUser.cs`

```csharp
// File: src/SubscriptionService.Api/Services/HttpCurrentUser.cs
using System.Security.Claims;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Api.Services;

/// <summary>Adapts HttpContext (JWT "sub" claim) to the application level ICurrentUser abstraction.</summary>
public sealed class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpCurrentUser(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public Guid UserId
    {
        get
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            var value = principal?.FindFirst("sub")?.Value
                        ?? principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(value, out var userId) || userId == Guid.Empty)
            {
                throw new UnauthorizedException("Access token does not contain a valid subject claim.");
            }

            return userId;
        }
    }
}
```

### `src/SubscriptionService.Api/SubscriptionService.Api.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <RootNamespace>SubscriptionService.Api</RootNamespace>
    <UserSecretsId>subscription-service-api</UserSecretsId>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.11" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.9.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\SubscriptionService.Application\SubscriptionService.Application.csproj" />
    <ProjectReference Include="..\SubscriptionService.Infrastructure\SubscriptionService.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

### `src/SubscriptionService.Api/appsettings.Development.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
}
```

### `src/SubscriptionService.Api/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Issuer": "SubscriptionService",
    "Audience": "SubscriptionService.Clients",
    "SigningKey": "dev-only-super-secret-signing-key-change-me-in-production-0123456789",
    "ExpirationMinutes": 60
  },
  "Database": {
    "Provider": "SqliteInMemory",
    "ConnectionString": "",
    "SeedDemoData": true
  },
  "Pricing": {
    "BasicMonthlyPrice": 10,
    "PremiumMonthlyPrice": 25,
    "CorporateMonthlyPrice": 100,
    "Currency": "USD"
  }
}
```

---

## Шаг 6. Юнит-тесты (xUnit + Moq + FluentAssertions)

### `tests/SubscriptionService.UnitTests/Common/TestData.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Common/TestData.cs
using Moq;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.UnitTests.Common;

internal static class TestData
{
    /// <summary>Frozen "now" - every time dependent assertion is deterministic.</summary>
    public static readonly DateTime UtcNow = new(2026, 3, 15, 10, 30, 0, DateTimeKind.Utc);

    public static User NewUser(Guid? id = null) =>
        new(id ?? Guid.NewGuid(), "user@subscriptions.local", "password-hash", "Test User", UtcNow.AddDays(-30));

    /// <summary>Active subscription that started <paramref name="monthsAgo"/> months ago.</summary>
    public static Subscription NewSubscription(Guid userId, int monthsAgo = 1, int billingPeriodInMonths = 3) =>
        Subscription.Create(
            userId,
            SubscriptionType.Premium,
            UtcNow.Date.AddMonths(-monthsAgo),
            billingPeriodInMonths,
            25m * billingPeriodInMonths,
            "USD");

    public static Mock<IAppTransaction> NewTransaction()
    {
        var transaction = new Mock<IAppTransaction>(MockBehavior.Strict);
        transaction.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        transaction.Setup(t => t.RollbackAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        transaction.Setup(t => t.DisposeAsync()).Returns(ValueTask.CompletedTask);
        return transaction;
    }
}
```

### `tests/SubscriptionService.UnitTests/Domain/PaymentAndUserTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Domain/PaymentAndUserTests.cs
using FluentAssertions;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.UnitTests.Common;
using Xunit;

namespace SubscriptionService.UnitTests.Domain;

public sealed class PaymentAndUserTests
{
    private static readonly DateTime UtcNow = TestData.UtcNow;

    [Fact]
    public void PaymentCreate_RoundsAmountAndKeepsPeriod()
    {
        var subscriptionId = Guid.NewGuid();

        var payment = Payment.Create(subscriptionId, 10.005m, "USD", UtcNow, UtcNow.AddMonths(1), UtcNow);

        payment.Id.Should().NotBeEmpty();
        payment.SubscriptionId.Should().Be(subscriptionId);
        payment.Amount.Should().Be(10.01m);
        payment.Currency.Should().Be("USD");
        payment.Status.Should().Be(PaymentStatus.Succeeded);
        payment.PeriodEnd.Should().Be(UtcNow.AddMonths(1));
    }

    [Fact]
    public void PaymentCreate_WithEmptySubscriptionId_Throws()
    {
        var act = () => Payment.Create(Guid.Empty, 10m, "USD", UtcNow, UtcNow.AddMonths(1), UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*SubscriptionId*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void PaymentCreate_WithNonPositiveAmount_Throws(decimal amount)
    {
        var act = () => Payment.Create(Guid.NewGuid(), amount, "USD", UtcNow, UtcNow.AddMonths(1), UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*amount must be positive*");
    }

    [Fact]
    public void PaymentCreate_WithEmptyCurrency_Throws()
    {
        var act = () => Payment.Create(Guid.NewGuid(), 10m, "", UtcNow, UtcNow.AddMonths(1), UtcNow);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void PaymentCreate_WithInvertedPeriod_Throws()
    {
        var act = () => Payment.Create(Guid.NewGuid(), 10m, "USD", UtcNow, UtcNow, UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*period end must be after*");
    }

    [Fact]
    public void PaymentCreate_WithFailedStatus_KeepsStatus()
    {
        var payment = Payment.Create(Guid.NewGuid(), 10m, "USD", UtcNow, UtcNow.AddMonths(1), UtcNow, PaymentStatus.Failed);

        payment.Status.Should().Be(PaymentStatus.Failed);
    }

    [Fact]
    public void UserConstructor_StoresDataAndStartsWithoutSubscriptions()
    {
        var id = Guid.NewGuid();

        var user = new User(id, "user@subscriptions.local", "hash", "User", UtcNow);

        user.Id.Should().Be(id);
        user.Email.Should().Be("user@subscriptions.local");
        user.PasswordHash.Should().Be("hash");
        user.Name.Should().Be("User");
        user.CreatedAtUtc.Should().Be(UtcNow);
        user.Subscriptions.Should().BeEmpty();
    }

    [Fact]
    public void UserConstructor_WithEmptyId_Throws()
    {
        var act = () => new User(Guid.Empty, "user@subscriptions.local", "hash", "User", UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*User id*");
    }

    [Theory]
    [InlineData("", "hash", "User")]
    [InlineData("user@subscriptions.local", "", "User")]
    [InlineData("user@subscriptions.local", "hash", " ")]
    public void UserConstructor_WithMissingRequiredData_Throws(string email, string hash, string name)
    {
        var act = () => new User(Guid.NewGuid(), email, hash, name, UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*required*");
    }
}
```

### `tests/SubscriptionService.UnitTests/Domain/SubscriptionTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Domain/SubscriptionTests.cs
using FluentAssertions;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.UnitTests.Common;
using Xunit;

namespace SubscriptionService.UnitTests.Domain;

public sealed class SubscriptionTests
{
    private static readonly DateTime UtcNow = TestData.UtcNow;
    private static readonly Guid UserId = Guid.NewGuid();

    private static Subscription NewSubscription(int months = 3, DateTime? startDate = null) =>
        Subscription.Create(UserId, SubscriptionType.Premium, startDate ?? UtcNow.Date, months, 25m * months, "USD");

    [Fact]
    public void Create_ProducesActiveSubscriptionForTheFirstPaidPeriod()
    {
        var subscription = NewSubscription();

        subscription.Id.Should().NotBeEmpty();
        subscription.UserId.Should().Be(UserId);
        subscription.Status.Should().Be(SubscriptionStatus.Active);
        subscription.CancelledAtUtc.Should().BeNull();
        subscription.StartDate.Should().Be(UtcNow.Date);
        subscription.CurrentPeriodEnd.Should().Be(UtcNow.Date.AddMonths(3));
        subscription.PricePerPeriod.Should().Be(75m);
        subscription.Payments.Should().BeEmpty();
    }

    [Fact]
    public void Create_RoundsPriceToTwoDecimals()
    {
        var subscription = Subscription.Create(UserId, SubscriptionType.Basic, UtcNow.Date, 1, 10.005m, "USD");

        subscription.PricePerPeriod.Should().Be(10.01m);
    }

    [Fact]
    public void Create_WithEmptyUserId_Throws()
    {
        var act = () => Subscription.Create(Guid.Empty, SubscriptionType.Basic, UtcNow.Date, 1, 10m, "USD");

        act.Should().Throw<DomainException>().WithMessage("*UserId*");
    }

    [Fact]
    public void Create_WithUnknownType_Throws()
    {
        var act = () => Subscription.Create(UserId, (SubscriptionType)7, UtcNow.Date, 1, 10m, "USD");

        act.Should().Throw<DomainException>().WithMessage("*Unknown subscription type*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void Create_WithInvalidBillingPeriod_Throws(int months)
    {
        var act = () => NewSubscription(months);

        act.Should().Throw<DomainException>().WithMessage("*Billing period*");
    }

    [Fact]
    public void Create_WithDefaultStartDate_Throws()
    {
        var act = () => NewSubscription(1, DateTime.MinValue);

        act.Should().Throw<DomainException>().WithMessage("*StartDate*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Create_WithNonPositivePrice_Throws(decimal price)
    {
        var act = () => Subscription.Create(UserId, SubscriptionType.Basic, UtcNow.Date, 1, price, "USD");

        act.Should().Throw<DomainException>().WithMessage("*Price must be positive*");
    }

    [Fact]
    public void Create_WithoutCurrency_Throws()
    {
        var act = () => Subscription.Create(UserId, SubscriptionType.Basic, UtcNow.Date, 1, 10m, " ");

        act.Should().Throw<DomainException>().WithMessage("*currency*");
    }

    [Fact]
    public void HasAccessAt_IsTrueUntilTheVeryEndOfThePaidPeriod()
    {
        var subscription = NewSubscription();

        subscription.HasAccessAt(subscription.CurrentPeriodEnd.AddTicks(-1)).Should().BeTrue();
        subscription.HasAccessAt(subscription.CurrentPeriodEnd).Should().BeFalse();
        subscription.HasAccessAt(subscription.CurrentPeriodEnd.AddDays(1)).Should().BeFalse();
    }

    [Fact]
    public void CreateInitialPayment_CoversTheWholeFirstPeriod()
    {
        var subscription = NewSubscription();

        var payment = subscription.CreateInitialPayment(UtcNow);

        payment.SubscriptionId.Should().Be(subscription.Id);
        payment.Amount.Should().Be(subscription.PricePerPeriod);
        payment.PeriodStart.Should().Be(subscription.StartDate);
        payment.PeriodEnd.Should().Be(subscription.CurrentPeriodEnd);
        payment.Status.Should().Be(PaymentStatus.Succeeded);
    }

    [Fact]
    public void Renew_ExtendsPaidPeriodAndReturnsPaymentForIt()
    {
        var subscription = NewSubscription();
        var previousEnd = subscription.CurrentPeriodEnd;

        var payment = subscription.Renew(UtcNow);

        subscription.CurrentPeriodEnd.Should().Be(previousEnd.AddMonths(3));
        payment.PeriodStart.Should().Be(previousEnd);
        payment.PeriodEnd.Should().Be(subscription.CurrentPeriodEnd);
        payment.Amount.Should().Be(75m);
    }

    [Fact]
    public void Renew_WhenCancelled_Throws()
    {
        var subscription = NewSubscription();
        subscription.Cancel(UtcNow);

        var act = () => subscription.Renew(UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*active subscription*");
    }

    [Fact]
    public void Cancel_KeepsPaidPeriodAndStampsCancellationTime()
    {
        var subscription = NewSubscription();
        var periodEnd = subscription.CurrentPeriodEnd;

        subscription.Cancel(UtcNow);

        subscription.Status.Should().Be(SubscriptionStatus.Cancelled);
        subscription.CancelledAtUtc.Should().Be(UtcNow);
        subscription.CurrentPeriodEnd.Should().Be(periodEnd);
        subscription.HasAccessAt(UtcNow).Should().BeTrue();
    }

    [Fact]
    public void Cancel_Twice_Throws()
    {
        var subscription = NewSubscription();
        subscription.Cancel(UtcNow);

        var act = () => subscription.Cancel(UtcNow);

        act.Should().Throw<DomainException>().WithMessage("*already cancelled*");
    }

    [Fact]
    public void Cancel_AfterPeriodEnd_Throws()
    {
        var subscription = NewSubscription(1);

        var act = () => subscription.Cancel(subscription.CurrentPeriodEnd);

        act.Should().Throw<DomainException>().WithMessage("*already ended*");
    }
}
```

### `tests/SubscriptionService.UnitTests/Services/AnalyticsServiceTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Services/AnalyticsServiceTests.cs
using FluentAssertions;
using Moq;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Common;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Application.Services;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.UnitTests.Common;
using Xunit;

namespace SubscriptionService.UnitTests.Services;

public sealed class AnalyticsServiceTests
{
    private static readonly DateTime UtcNow = TestData.UtcNow; // 2026-03-15

    private readonly Mock<IPaymentRepository> _payments = new();
    private readonly Mock<ICurrentUser> _currentUser = new();
    private readonly Mock<ISubscriptionPricing> _pricing = new();
    private readonly Mock<IClock> _clock = new();
    private readonly Guid _userId = Guid.NewGuid();
    private readonly AnalyticsService _sut;

    public AnalyticsServiceTests()
    {
        _clock.SetupGet(c => c.UtcNow).Returns(UtcNow);
        _currentUser.SetupGet(c => c.UserId).Returns(_userId);
        _pricing.SetupGet(p => p.Currency).Returns("USD");
        _payments.Setup(r => r.GetExpensesAsync(_userId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SubscriptionExpense>());

        _sut = new AnalyticsService(_payments.Object, _currentUser.Object, _pricing.Object, _clock.Object);
    }

    private void SetupExpenses(params SubscriptionExpense[] expenses) =>
        _payments.Setup(r => r.GetExpensesAsync(_userId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expenses.ToList());

    private static SubscriptionExpense Expense(SubscriptionType type, decimal amount, int year, int month, int day = 10) =>
        new(Guid.NewGuid(), type, amount, new DateTime(year, month, day, 12, 0, 0, DateTimeKind.Utc));

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(37)]
    [InlineData(int.MaxValue)]
    public async Task GetExpensesAsync_WithInvalidMonths_ThrowsValidation(int months)
    {
        Func<Task> act = () => _sut.GetExpensesAsync(months);

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey("months");
        _payments.Verify(r => r.GetExpensesAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetExpensesAsync_QueriesExactCalendarMonthRangeOfCurrentUser()
    {
        await _sut.GetExpensesAsync(3);

        _payments.Verify(r => r.GetExpensesAsync(
            _userId,
            new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetExpensesAsync_GroupsByMonthAndType_AndComputesTotals()
    {
        SetupExpenses(
            Expense(SubscriptionType.Basic, 10m, 2026, 1),
            Expense(SubscriptionType.Basic, 10m, 2026, 1, 25),
            Expense(SubscriptionType.Premium, 25m, 2026, 2),
            Expense(SubscriptionType.Corporate, 100m, 2026, 3));

        var report = await _sut.GetExpensesAsync(3);

        report.Months.Should().Be(3);
        report.From.Should().Be(new DateOnly(2026, 1, 1));
        report.To.Should().Be(new DateOnly(2026, 3, 31));
        report.Currency.Should().Be("USD");
        report.Total.Should().Be(145m);
        report.PaymentsCount.Should().Be(4);

        report.ByMonth.Should().HaveCount(3);
        report.ByMonth[0].Period.Should().Be("2026-01");
        report.ByMonth[0].Total.Should().Be(20m);
        report.ByMonth[0].Breakdown.Should().ContainSingle()
            .Which.Should().BeEquivalentTo(new { Type = SubscriptionType.Basic, TypeName = "Basic", Amount = 20m, PaymentsCount = 2 });
        report.ByMonth[1].Total.Should().Be(25m);
        report.ByMonth[2].Total.Should().Be(100m);

        report.ByType.Should().HaveCount(3);
        report.ByType.Select(t => t.Type).Should().BeInAscendingOrder();
        report.ByType.Sum(t => t.Amount).Should().Be(report.Total);
    }

    [Fact]
    public async Task GetExpensesAsync_FillsMonthsWithoutPaymentsWithZero()
    {
        SetupExpenses(Expense(SubscriptionType.Premium, 25m, 2026, 3));

        var report = await _sut.GetExpensesAsync(6);

        report.ByMonth.Should().HaveCount(6);
        report.ByMonth.Take(5).Should().OnlyContain(m => m.Total == 0m && m.Breakdown.Count == 0);
        report.ByMonth[5].Period.Should().Be("2026-03");
        report.ByMonth[5].Total.Should().Be(25m);
        report.ByMonth[0].Period.Should().Be("2025-10");
        report.Total.Should().Be(25m);
    }

    [Fact]
    public async Task GetExpensesAsync_WithoutPayments_ReturnsEmptyReport()
    {
        var report = await _sut.GetExpensesAsync(6);

        report.Total.Should().Be(0m);
        report.PaymentsCount.Should().Be(0);
        report.ByType.Should().BeEmpty();
        report.ByMonth.Should().HaveCount(6).And.OnlyContain(m => m.Total == 0m);
    }

    [Fact]
    public async Task GetExpensesAsync_WithSingleMonth_UsesCurrentMonthOnly()
    {
        SetupExpenses(Expense(SubscriptionType.Basic, 10m, 2026, 3));

        var report = await _sut.GetExpensesAsync(1);

        report.ByMonth.Should().ContainSingle().Which.Period.Should().Be("2026-03");
        report.From.Should().Be(new DateOnly(2026, 3, 1));
        report.To.Should().Be(new DateOnly(2026, 3, 31));
    }

    [Fact]
    public async Task GetExpensesAsync_CrossesYearBoundaryCorrectly()
    {
        _clock.SetupGet(c => c.UtcNow).Returns(new DateTime(2026, 1, 15, 8, 0, 0, DateTimeKind.Utc));
        SetupExpenses(
            Expense(SubscriptionType.Basic, 10m, 2025, 11),
            Expense(SubscriptionType.Premium, 25m, 2025, 12),
            Expense(SubscriptionType.Premium, 25m, 2026, 1));

        var report = await _sut.GetExpensesAsync(3);

        report.ByMonth.Select(m => m.Period).Should().Equal("2025-11", "2025-12", "2026-01");
        report.Total.Should().Be(60m);
        report.From.Should().Be(new DateOnly(2025, 11, 1));
        report.To.Should().Be(new DateOnly(2026, 1, 31));
    }

    [Fact]
    public async Task GetExpensesAsync_RoundsMoneyToTwoDecimals()
    {
        SetupExpenses(
            Expense(SubscriptionType.Basic, 10.004m, 2026, 3),
            Expense(SubscriptionType.Basic, 10.004m, 2026, 3));

        var report = await _sut.GetExpensesAsync(1);

        report.Total.Should().Be(20.01m);
        report.ByMonth[0].Total.Should().Be(20.01m);
    }

    [Fact]
    public async Task GetExpensesAsync_AcceptsUpperBoundaryOf36Months()
    {
        var report = await _sut.GetExpensesAsync(36);

        report.ByMonth.Should().HaveCount(36);
        report.From.Should().Be(new DateOnly(2023, 4, 1));
    }
}
```

### `tests/SubscriptionService.UnitTests/Services/AuthServiceTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Services/AuthServiceTests.cs
using FluentAssertions;
using Moq;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Services;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.UnitTests.Common;
using Xunit;

namespace SubscriptionService.UnitTests.Services;

public sealed class AuthServiceTests
{
    private static readonly DateTime UtcNow = TestData.UtcNow;

    private readonly Mock<IUserRepository> _users = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly Mock<IJwtTokenGenerator> _tokenGenerator = new();
    private readonly Mock<IClock> _clock = new();
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _clock.SetupGet(c => c.UtcNow).Returns(UtcNow);
        _passwordHasher.Setup(h => h.Hash(It.IsAny<string>())).Returns("hashed-password");
        _tokenGenerator.Setup(t => t.Generate(It.IsAny<User>()))
            .Returns(new JwtToken("jwt-token", UtcNow.AddMinutes(60)));
        _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _sut = new AuthService(_users.Object, _unitOfWork.Object, _passwordHasher.Object, _tokenGenerator.Object, _clock.Object);
    }

    // ---------------------------------------------------------------- REGISTER

    [Fact]
    public async Task RegisterAsync_WithValidRequest_StoresHashedPasswordAndReturnsToken()
    {
        var request = new RegisterRequest("Alice@Example.com", "Passw0rd!", " Alice ");

        var response = await _sut.RegisterAsync(request);

        response.AccessToken.Should().Be("jwt-token");
        response.TokenType.Should().Be("Bearer");
        response.ExpiresAtUtc.Should().Be(UtcNow.AddMinutes(60));
        response.User.Email.Should().Be("alice@example.com");
        response.User.Name.Should().Be("Alice");
        response.User.Id.Should().NotBeEmpty();

        var added = new List<User>();
        _users.Verify(r => r.AddAsync(Capture.In(added), It.IsAny<CancellationToken>()), Times.Once);
        added.Single().PasswordHash.Should().Be("hashed-password");
        added.Single().Email.Should().Be("alice@example.com");
        added.Single().CreatedAtUtc.Should().Be(UtcNow);

        _passwordHasher.Verify(h => h.Hash("Passw0rd!"), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _tokenGenerator.Verify(t => t.Generate(It.Is<User>(u => u.Email == "alice@example.com")), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_WhenEmailAlreadyTaken_ThrowsConflictAndStoresNothing()
    {
        _users.Setup(r => r.ExistsByEmailAsync("alice@example.com", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest("alice@example.com", "Passw0rd!", "Alice"));

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already registered*");

        _users.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _passwordHasher.Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-an-email")]
    [InlineData("missing@domain")]
    [InlineData("two@@domain.com")]
    [InlineData("spaces in@domain.com")]
    public async Task RegisterAsync_WithInvalidEmail_ThrowsValidation(string email)
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest(email, "Passw0rd!", "Alice"));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey("Email");
        _users.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Sh0rt")]
    [InlineData("onlyletters")]
    [InlineData("12345678")]
    public async Task RegisterAsync_WithWeakPassword_ThrowsValidation(string password)
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest("alice@example.com", password, "Alice"));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey("Password");
        _passwordHasher.Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WithTooLongPassword_ThrowsValidation()
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest("alice@example.com", new string('a', 120) + "123456789", "Alice"));

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A")]
    public async Task RegisterAsync_WithInvalidName_ThrowsValidation(string name)
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest("alice@example.com", "Passw0rd!", name));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey("Name");
    }

    [Fact]
    public async Task RegisterAsync_WithSeveralInvalidFields_ReportsAllErrors()
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest("bad", "bad", ""));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Keys.Should().BeEquivalentTo(new[] { "Email", "Password", "Name" });
    }

    [Fact]
    public async Task RegisterAsync_WithNullFields_ThrowsValidationForEveryField()
    {
        Func<Task> act = () => _sut.RegisterAsync(new RegisterRequest(null!, null!, null!));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Keys.Should().BeEquivalentTo(new[] { "Email", "Password", "Name" });
        _users.Verify(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    // ---------------------------------------------------------------- LOGIN

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsToken()
    {
        var user = TestData.NewUser();
        _users.Setup(r => r.GetByEmailAsync("user@subscriptions.local", It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _passwordHasher.Setup(h => h.Verify(user.PasswordHash, "Passw0rd!")).Returns(true);

        var response = await _sut.LoginAsync(new LoginRequest(" User@Subscriptions.Local ", "Passw0rd!"));

        response.AccessToken.Should().Be("jwt-token");
        response.User.Id.Should().Be(user.Id);
        _users.Verify(r => r.GetByEmailAsync("user@subscriptions.local", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WhenUserNotFound_ThrowsUnauthorizedWithoutHashVerification()
    {
        _users.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        Func<Task> act = () => _sut.LoginAsync(new LoginRequest("ghost@example.com", "Passw0rd!"));

        await act.Should().ThrowAsync<UnauthorizedException>().WithMessage("Invalid e-mail or password.");
        _passwordHasher.Verify(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _tokenGenerator.Verify(t => t.Generate(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_WithWrongPassword_ThrowsUnauthorizedAndIssuesNoToken()
    {
        var user = TestData.NewUser();
        _users.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _passwordHasher.Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        Func<Task> act = () => _sut.LoginAsync(new LoginRequest(user.Email, "WrongPass1"));

        await act.Should().ThrowAsync<UnauthorizedException>().WithMessage("Invalid e-mail or password.");
        _tokenGenerator.Verify(t => t.Generate(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_WithNullCredentials_ThrowsValidation()
    {
        Func<Task> act = () => _sut.LoginAsync(new LoginRequest(null!, null!));

        await act.Should().ThrowAsync<ValidationException>();
        _users.Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData("", "Passw0rd!")]
    [InlineData("alice@example.com", "")]
    public async Task LoginAsync_WithMissingCredentials_ThrowsValidation(string email, string password)
    {
        Func<Task> act = () => _sut.LoginAsync(new LoginRequest(email, password));

        await act.Should().ThrowAsync<ValidationException>();
        _users.Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
```

### `tests/SubscriptionService.UnitTests/Services/SubscriptionPricingTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Services/SubscriptionPricingTests.cs
using FluentAssertions;
using Microsoft.Extensions.Options;
using SubscriptionService.Application.Options;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using Xunit;

namespace SubscriptionService.UnitTests.Services;

public sealed class SubscriptionPricingTests
{
    [Theory]
    [InlineData(SubscriptionType.Basic, 10)]
    [InlineData(SubscriptionType.Premium, 25)]
    [InlineData(SubscriptionType.Corporate, 100)]
    public void GetMonthlyPrice_UsesDefaultTariffs(SubscriptionType type, decimal expected)
    {
        var sut = new SubscriptionPricing(Options.Create(new PricingOptions()));

        sut.GetMonthlyPrice(type).Should().Be(expected);
        sut.Currency.Should().Be("USD");
    }

    [Fact]
    public void GetMonthlyPrice_UsesConfiguredTariffs()
    {
        var sut = new SubscriptionPricing(Options.Create(new PricingOptions
        {
            BasicMonthlyPrice = 12.5m,
            PremiumMonthlyPrice = 30m,
            CorporateMonthlyPrice = 120m,
            Currency = "EUR"
        }));

        sut.GetMonthlyPrice(SubscriptionType.Basic).Should().Be(12.5m);
        sut.GetMonthlyPrice(SubscriptionType.Premium).Should().Be(30m);
        sut.GetMonthlyPrice(SubscriptionType.Corporate).Should().Be(120m);
        sut.Currency.Should().Be("EUR");
    }

    [Fact]
    public void GetMonthlyPrice_WithUnknownType_ThrowsValidation()
    {
        var sut = new SubscriptionPricing(Options.Create(new PricingOptions()));

        var act = () => sut.GetMonthlyPrice((SubscriptionType)999);

        act.Should().Throw<ValidationException>();
    }
}
```

### `tests/SubscriptionService.UnitTests/Services/SubscriptionServiceTests.cs`

```csharp
// File: tests/SubscriptionService.UnitTests/Services/SubscriptionServiceTests.cs
using FluentAssertions;
using Moq;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Pricing;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.UnitTests.Common;
using Xunit;
// Alias: the class name equals the root namespace, so an explicit alias keeps the compiler happy.
using SubscriptionAppService = SubscriptionService.Application.Services.SubscriptionService;

namespace SubscriptionService.UnitTests.Services;

public sealed class SubscriptionServiceTests
{
    private static readonly DateTime UtcNow = TestData.UtcNow;

    private readonly Mock<ISubscriptionRepository> _subscriptions = new();
    private readonly Mock<IPaymentRepository> _payments = new();
    private readonly Mock<IUserRepository> _users = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<ISubscriptionPricing> _pricing = new();
    private readonly Mock<ICurrentUser> _currentUser = new();
    private readonly Mock<IClock> _clock = new();
    private readonly Mock<IAppTransaction> _transaction = TestData.NewTransaction();
    private readonly Guid _userId = Guid.NewGuid();
    private readonly SubscriptionAppService _sut;

    public SubscriptionServiceTests()
    {
        _clock.SetupGet(c => c.UtcNow).Returns(UtcNow);
        _currentUser.SetupGet(c => c.UserId).Returns(_userId);

        _pricing.SetupGet(p => p.Currency).Returns("USD");
        _pricing.Setup(p => p.GetMonthlyPrice(SubscriptionType.Basic)).Returns(10m);
        _pricing.Setup(p => p.GetMonthlyPrice(SubscriptionType.Premium)).Returns(25m);
        _pricing.Setup(p => p.GetMonthlyPrice(SubscriptionType.Corporate)).Returns(100m);

        _users.Setup(r => r.GetByIdAsync(_userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestData.NewUser(_userId));

        _subscriptions.Setup(r => r.HasRunningSubscriptionAsync(_userId, UtcNow, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _unitOfWork.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transaction.Object);
        _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        _sut = new SubscriptionAppService(
            _subscriptions.Object,
            _payments.Object,
            _users.Object,
            _unitOfWork.Object,
            _pricing.Object,
            _currentUser.Object,
            _clock.Object);
    }

    private static CreateSubscriptionRequest ValidRequest(
        SubscriptionType type = SubscriptionType.Premium,
        int months = 6,
        DateTime? startDate = null) =>
        new(type, startDate ?? UtcNow.Date, months);

    // ---------------------------------------------------------------- CREATE: happy path

    [Fact]
    public async Task CreateAsync_WithValidRequest_PersistsSubscriptionAndPaymentInOneTransaction()
    {
        var request = ValidRequest();

        var result = await _sut.CreateAsync(request);

        result.Id.Should().NotBeEmpty();
        result.Type.Should().Be(SubscriptionType.Premium);
        result.TypeName.Should().Be("Premium");
        result.Status.Should().Be(SubscriptionStatus.Active);
        result.StatusName.Should().Be("Active");
        result.StartDate.Should().Be(UtcNow.Date);
        result.BillingPeriodInMonths.Should().Be(6);
        result.PricePerPeriod.Should().Be(150m); // 25 $ * 6 months
        result.Currency.Should().Be("USD");
        result.CurrentPeriodEnd.Should().Be(UtcNow.Date.AddMonths(6));
        result.CancelledAtUtc.Should().BeNull();
        result.HasAccess.Should().BeTrue();

        var subscription = CaptureAddedSubscription();
        var payment = CaptureAddedPayment();

        subscription.UserId.Should().Be(_userId);
        payment.SubscriptionId.Should().Be(subscription.Id);
        payment.Amount.Should().Be(subscription.PricePerPeriod);
        payment.Currency.Should().Be("USD");
        payment.Status.Should().Be(PaymentStatus.Succeeded);
        payment.PeriodStart.Should().Be(subscription.StartDate);
        payment.PeriodEnd.Should().Be(subscription.CurrentPeriodEnd);
        payment.PaidAtUtc.Should().Be(UtcNow);

        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
        _transaction.Verify(t => t.DisposeAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WritesInOrder_BeginTransaction_AddSubscription_AddPayment_Save_Commit()
    {
        var calls = new List<string>();

        _unitOfWork.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("begin"))
            .ReturnsAsync(_transaction.Object);
        _subscriptions.Setup(r => r.AddAsync(It.IsAny<Subscription>(), It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("add-subscription"))
            .Returns(Task.CompletedTask);
        _payments.Setup(r => r.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("add-payment"))
            .Returns(Task.CompletedTask);
        _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("save"))
            .ReturnsAsync(2);
        _transaction.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("commit"))
            .Returns(Task.CompletedTask);

        await _sut.CreateAsync(ValidRequest());

        calls.Should().Equal("begin", "add-subscription", "add-payment", "save", "commit");
    }

    [Theory]
    [InlineData(SubscriptionType.Basic, 1, 10)]
    [InlineData(SubscriptionType.Basic, 12, 120)]
    [InlineData(SubscriptionType.Premium, 3, 75)]
    [InlineData(SubscriptionType.Premium, 6, 150)]
    [InlineData(SubscriptionType.Corporate, 6, 600)]
    [InlineData(SubscriptionType.Corporate, 12, 1200)]
    public async Task CreateAsync_ComputesPriceAsMonthlyPriceTimesMonths(SubscriptionType type, int months, decimal expectedPrice)
    {
        var result = await _sut.CreateAsync(ValidRequest(type, months));

        result.PricePerPeriod.Should().Be(expectedPrice);
        result.CurrentPeriodEnd.Should().Be(UtcNow.Date.AddMonths(months));
        CaptureAddedPayment().Amount.Should().Be(expectedPrice);
        _pricing.Verify(p => p.GetMonthlyPrice(type), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WithPastStartDate_KeepsRequestedStartDateAndComputesPeriodFromIt()
    {
        var startDate = UtcNow.Date.AddMonths(-2);

        var result = await _sut.CreateAsync(ValidRequest(months: 3, startDate: startDate));

        result.StartDate.Should().Be(startDate);
        result.CurrentPeriodEnd.Should().Be(startDate.AddMonths(3));
        result.HasAccess.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_NormalizesStartDateToUtcMidnight()
    {
        var localEvening = new DateTime(2026, 3, 15, 18, 45, 0, DateTimeKind.Unspecified);

        var result = await _sut.CreateAsync(ValidRequest(startDate: localEvening));

        result.StartDate.Should().Be(new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc));
        result.StartDate.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public async Task CreateAsync_ForwardsCancellationTokenToPersistence()
    {
        using var cts = new CancellationTokenSource();

        await _sut.CreateAsync(ValidRequest(), cts.Token);

        _users.Verify(r => r.GetByIdAsync(_userId, cts.Token), Times.Once);
        _subscriptions.Verify(r => r.AddAsync(It.IsAny<Subscription>(), cts.Token), Times.Once);
        _payments.Verify(r => r.AddAsync(It.IsAny<Payment>(), cts.Token), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(cts.Token), Times.Once);
    }

    // ---------------------------------------------------------------- CREATE: validation

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(13)]
    [InlineData(100)]
    public async Task CreateAsync_WithOutOfRangeBillingPeriod_ThrowsValidationAndWritesNothing(int months)
    {
        Func<Task> act = () => _sut.CreateAsync(ValidRequest(months: months));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey(nameof(CreateSubscriptionRequest.BillingPeriodInMonths));

        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WithUnknownSubscriptionType_ThrowsValidationAndWritesNothing()
    {
        Func<Task> act = () => _sut.CreateAsync(ValidRequest((SubscriptionType)99));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey(nameof(CreateSubscriptionRequest.Type));

        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WithDefaultStartDate_ThrowsValidationAndWritesNothing()
    {
        Func<Task> act = () => _sut.CreateAsync(ValidRequest(startDate: DateTime.MinValue));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey(nameof(CreateSubscriptionRequest.StartDate));

        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WithFutureStartDate_ThrowsValidationAndWritesNothing()
    {
        Func<Task> act = () => _sut.CreateAsync(ValidRequest(startDate: UtcNow.Date.AddDays(1)));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors[nameof(CreateSubscriptionRequest.StartDate)].Should().Contain("StartDate cannot be in the future.");

        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WithSeveralInvalidFields_ReportsAllErrorsAtOnce()
    {
        Func<Task> act = () => _sut.CreateAsync(new CreateSubscriptionRequest((SubscriptionType)42, default, 0));

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;

        exception.Errors.Keys.Should().BeEquivalentTo(new[]
        {
            nameof(CreateSubscriptionRequest.Type),
            nameof(CreateSubscriptionRequest.BillingPeriodInMonths),
            nameof(CreateSubscriptionRequest.StartDate)
        });

        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WithNullRequest_ThrowsArgumentNull()
    {
        Func<Task> act = () => _sut.CreateAsync(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
        VerifyNothingWasWritten();
    }

    // ---------------------------------------------------------------- CREATE: business rules

    [Fact]
    public async Task CreateAsync_WhenUserDoesNotExist_ThrowsNotFound()
    {
        _users.Setup(r => r.GetByIdAsync(_userId, It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        Func<Task> act = () => _sut.CreateAsync(ValidRequest());

        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"*{_userId}*");
        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WhenUserAlreadyHasRunningSubscription_ThrowsConflict()
    {
        _subscriptions.Setup(r => r.HasRunningSubscriptionAsync(_userId, UtcNow, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        Func<Task> act = () => _sut.CreateAsync(ValidRequest());

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already has an active subscription*");
        VerifyNothingWasWritten();
    }

    [Fact]
    public async Task CreateAsync_WhenSaveChangesFails_RollsBackAndRethrows()
    {
        _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("database is down"));

        Func<Task> act = () => _sut.CreateAsync(ValidRequest());

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("database is down");

        _transaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        _transaction.Verify(t => t.DisposeAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenPaymentCannotBeStored_RollsBackAndRethrows()
    {
        _payments.Setup(r => r.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("payment store failure"));

        Func<Task> act = () => _sut.CreateAsync(ValidRequest());

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("payment store failure");

        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _transaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // ---------------------------------------------------------------- GET ACTIVE

    [Fact]
    public async Task GetActiveAsync_ReturnsActiveAndCancelledWithAccess_OrderedByStartDateDescending()
    {
        var older = TestData.NewSubscription(_userId, monthsAgo: 3, billingPeriodInMonths: 6);
        var newer = TestData.NewSubscription(_userId, monthsAgo: 1, billingPeriodInMonths: 3);
        newer.Cancel(UtcNow.AddDays(-1));

        _subscriptions.Setup(r => r.GetWithAccessByUserAsync(_userId, UtcNow, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Subscription> { older, newer });

        var result = await _sut.GetActiveAsync();

        result.Should().HaveCount(2);
        result[0].Id.Should().Be(newer.Id);
        result[0].Status.Should().Be(SubscriptionStatus.Cancelled);
        result[0].HasAccess.Should().BeTrue();
        result[0].CancelledAtUtc.Should().Be(UtcNow.AddDays(-1));
        result[1].Id.Should().Be(older.Id);
        result[1].Status.Should().Be(SubscriptionStatus.Active);
    }

    [Fact]
    public async Task GetActiveAsync_WhenUserHasNoSubscriptions_ReturnsEmptyList()
    {
        _subscriptions.Setup(r => r.GetWithAccessByUserAsync(_userId, UtcNow, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Subscription>());

        var result = await _sut.GetActiveAsync();

        result.Should().BeEmpty();
        _subscriptions.Verify(r => r.GetWithAccessByUserAsync(_userId, UtcNow, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ---------------------------------------------------------------- CANCEL

    [Fact]
    public async Task CancelAsync_WithOwnedActiveSubscription_CancelsButKeepsAccessUntilPeriodEnd()
    {
        var subscription = TestData.NewSubscription(_userId, monthsAgo: 1, billingPeriodInMonths: 3);
        var periodEndBefore = subscription.CurrentPeriodEnd;
        _subscriptions.Setup(r => r.GetByIdAsync(subscription.Id, It.IsAny<CancellationToken>())).ReturnsAsync(subscription);

        var result = await _sut.CancelAsync(subscription.Id);

        result.Status.Should().Be(SubscriptionStatus.Cancelled);
        result.StatusName.Should().Be("Cancelled");
        result.CancelledAtUtc.Should().Be(UtcNow);
        result.CurrentPeriodEnd.Should().Be(periodEndBefore); // paid period is never shortened
        result.HasAccess.Should().BeTrue();

        subscription.Status.Should().Be(SubscriptionStatus.Cancelled);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CancelAsync_DoesNotNeedExplicitTransaction()
    {
        var subscription = TestData.NewSubscription(_userId);
        _subscriptions.Setup(r => r.GetByIdAsync(subscription.Id, It.IsAny<CancellationToken>())).ReturnsAsync(subscription);

        await _sut.CancelAsync(subscription.Id);

        _unitOfWork.Verify(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CancelAsync_WhenSubscriptionDoesNotExist_ThrowsNotFound()
    {
        var id = Guid.NewGuid();
        _subscriptions.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Subscription?)null);

        Func<Task> act = () => _sut.CancelAsync(id);

        await act.Should().ThrowAsync<NotFoundException>();
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CancelAsync_WithEmptyId_ThrowsNotFound()
    {
        Func<Task> act = () => _sut.CancelAsync(Guid.Empty);

        await act.Should().ThrowAsync<NotFoundException>();
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CancelAsync_WhenSubscriptionBelongsToAnotherUser_ThrowsNotFoundAndDoesNotCancel()
    {
        var foreign = TestData.NewSubscription(Guid.NewGuid());
        _subscriptions.Setup(r => r.GetByIdAsync(foreign.Id, It.IsAny<CancellationToken>())).ReturnsAsync(foreign);

        Func<Task> act = () => _sut.CancelAsync(foreign.Id);

        await act.Should().ThrowAsync<NotFoundException>();
        foreign.Status.Should().Be(SubscriptionStatus.Active);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CancelAsync_WhenAlreadyCancelled_ThrowsConflict()
    {
        var subscription = TestData.NewSubscription(_userId);
        subscription.Cancel(UtcNow.AddDays(-2));
        _subscriptions.Setup(r => r.GetByIdAsync(subscription.Id, It.IsAny<CancellationToken>())).ReturnsAsync(subscription);

        Func<Task> act = () => _sut.CancelAsync(subscription.Id);

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already cancelled*");
        subscription.CancelledAtUtc.Should().Be(UtcNow.AddDays(-2)); // original cancellation timestamp is preserved
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CancelAsync_WhenPaidPeriodAlreadyEnded_ThrowsConflict()
    {
        var expired = TestData.NewSubscription(_userId, monthsAgo: 4, billingPeriodInMonths: 1);
        _subscriptions.Setup(r => r.GetByIdAsync(expired.Id, It.IsAny<CancellationToken>())).ReturnsAsync(expired);

        Func<Task> act = () => _sut.CancelAsync(expired.Id);

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already ended*");
        expired.Status.Should().Be(SubscriptionStatus.Active);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // ---------------------------------------------------------------- helpers

    private Subscription CaptureAddedSubscription()
    {
        var captured = new List<Subscription>();
        _subscriptions.Verify(r => r.AddAsync(Capture.In(captured), It.IsAny<CancellationToken>()), Times.Once);
        return captured.Single();
    }

    private Payment CaptureAddedPayment()
    {
        var captured = new List<Payment>();
        _payments.Verify(r => r.AddAsync(Capture.In(captured), It.IsAny<CancellationToken>()), Times.Once);
        return captured.Single();
    }

    private void VerifyNothingWasWritten()
    {
        _subscriptions.Verify(r => r.AddAsync(It.IsAny<Subscription>(), It.IsAny<CancellationToken>()), Times.Never);
        _payments.Verify(r => r.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
```

### `tests/SubscriptionService.UnitTests/SubscriptionService.UnitTests.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
    <PackageReference Include="xunit" Version="2.9.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Moq" Version="4.20.72" />
    <PackageReference Include="FluentAssertions" Version="6.12.2" />
    <PackageReference Include="coverlet.collector" Version="6.0.2">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\src\SubscriptionService.Application\SubscriptionService.Application.csproj" />
  </ItemGroup>
</Project>
```
