// File: src/SubscriptionService.Application/DTOs/AuthDtos.cs
namespace SubscriptionService.Application.DTOs;

/// <summary>POST /api/auth/register payload.</summary>
public sealed record RegisterRequest(string Email, string Password, string Name);

/// <summary>POST /api/auth/login payload.</summary>
public sealed record LoginRequest(string Email, string Password);

public sealed record UserDto(Guid Id, string Email, string Name);

/// <summary>Issued JWT + its lifetime + the user profile.</summary>
public sealed record AuthResponse(string AccessToken, string TokenType, DateTime ExpiresAtUtc, UserDto User);
