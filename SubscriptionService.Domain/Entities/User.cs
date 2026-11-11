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
