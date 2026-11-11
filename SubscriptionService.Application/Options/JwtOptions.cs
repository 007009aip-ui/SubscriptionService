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
