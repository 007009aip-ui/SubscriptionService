// File: src/SubscriptionService.Application/Abstractions/Services/IAuthService.cs
using SubscriptionService.Application.DTOs;

namespace SubscriptionService.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
