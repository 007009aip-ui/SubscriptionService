// File: src/SubscriptionService.Application/Abstractions/IClock.cs
namespace SubscriptionService.Application.Abstractions;

/// <summary>Time is an external dependency - injecting it makes time dependent logic testable.</summary>
public interface IClock
{
    DateTime UtcNow { get; }
}
