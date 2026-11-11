// File: src/SubscriptionService.Application/Services/AuthService.cs
using System.Text.RegularExpressions;
using SubscriptionService.Application.Abstractions;
using SubscriptionService.Application.Abstractions.Persistence;
using SubscriptionService.Application.Abstractions.Services;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Mappings;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Exceptions;

namespace SubscriptionService.Application.Services;

public sealed class AuthService : IAuthService
{
    private const int MinPasswordLength = 8;
    private const int MaxPasswordLength = 128;
    private const int MinNameLength = 2;
    private const int MaxNameLength = 100;

    private static readonly Regex EmailPattern = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IClock _clock;

    public AuthService(
        IUserRepository users,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator,
        IClock clock)
    {
        _users = users;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _clock = clock;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var email = NormalizeEmail(request.Email);
        var name = request.Name?.Trim() ?? string.Empty;
        var errors = new Dictionary<string, string[]>();

        if (!EmailPattern.IsMatch(email))
        {
            errors[nameof(request.Email)] = new[] { "A valid e-mail address is required." };
        }

        var passwordErrors = ValidatePassword(request.Password);
        if (passwordErrors.Length > 0)
        {
            errors[nameof(request.Password)] = passwordErrors;
        }

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors[nameof(request.Name)] = new[] { $"Name must be between {MinNameLength} and {MaxNameLength} characters." };
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        if (await _users.ExistsByEmailAsync(email, cancellationToken))
        {
            throw new ConflictException($"User with e-mail '{email}' is already registered.");
        }

        var user = new User(Guid.NewGuid(), email, _passwordHasher.Hash(request.Password), name, _clock.UtcNow);

        await _users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return BuildAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var email = NormalizeEmail(request.Email);
        if (email.Length == 0 || string.IsNullOrEmpty(request.Password))
        {
            throw new ValidationException(nameof(request.Email), "E-mail and password are required.");
        }

        var user = await _users.GetByEmailAsync(email, cancellationToken);

        // Same message for unknown e-mail and wrong password: do not leak which accounts exist.
        if (user is null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedException("Invalid e-mail or password.");
        }

        return BuildAuthResponse(user);
    }

    private static string NormalizeEmail(string? email) => email?.Trim().ToLowerInvariant() ?? string.Empty;

    private static string[] ValidatePassword(string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return new[] { "Password is required." };
        }

        var errors = new List<string>();
        if (password.Length is < MinPasswordLength or > MaxPasswordLength)
        {
            errors.Add($"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters.");
        }

        if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
        {
            errors.Add("Password must contain at least one letter and one digit.");
        }

        return errors.ToArray();
    }

    private AuthResponse BuildAuthResponse(User user)
    {
        var token = _tokenGenerator.Generate(user);
        return new AuthResponse(token.AccessToken, "Bearer", token.ExpiresAtUtc, user.ToDto());
    }
}
