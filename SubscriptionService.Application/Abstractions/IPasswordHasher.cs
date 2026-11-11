// File: src/SubscriptionService.Application/Abstractions/IPasswordHasher.cs
namespace SubscriptionService.Application.Abstractions;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string passwordHash, string password);
}
