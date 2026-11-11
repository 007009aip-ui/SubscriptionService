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
