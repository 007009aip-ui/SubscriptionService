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
