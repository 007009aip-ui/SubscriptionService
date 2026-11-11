// File: src/SubscriptionService.Application/Abstractions/ICurrentUser.cs
namespace SubscriptionService.Application.Abstractions;

/// <summary>Ambient user of the current request, resolved from the JWT by the API layer.</summary>
public interface ICurrentUser
{
    Guid UserId { get; }
}
