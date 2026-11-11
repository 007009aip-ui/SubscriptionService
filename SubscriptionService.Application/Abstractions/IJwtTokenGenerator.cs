// File: src/SubscriptionService.Application/Abstractions/IJwtTokenGenerator.cs
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Abstractions;

public sealed record JwtToken(string AccessToken, DateTime ExpiresAtUtc);

public interface IJwtTokenGenerator
{
    JwtToken Generate(User user);
}
