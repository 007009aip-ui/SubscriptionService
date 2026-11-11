// File: src/SubscriptionService.Domain/Exceptions/AppExceptions.cs
namespace SubscriptionService.Domain.Exceptions;

/// <summary>Base class for expected (business) failures. Mapped to HTTP codes by the API middleware.</summary>
public abstract class AppException : Exception
{
    protected AppException(string message) : base(message)
    {
    }

    /// <summary>Stable machine readable error code returned in ProblemDetails.</summary>
    public abstract string Code { get; }
}

/// <summary>Invalid input -> HTTP 400.</summary>
public sealed class ValidationException : AppException
{
    public ValidationException(string field, string error)
        : this(new Dictionary<string, string[]> { [field] = new[] { error } })
    {
    }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public override string Code => "validation_error";
}

/// <summary>Missing resource (or a resource owned by somebody else) -> HTTP 404.</summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string entity, object key)
        : base($"{entity} with key '{key}' was not found.")
    {
    }

    public override string Code => "not_found";
}

/// <summary>Business rule conflict -> HTTP 409.</summary>
public sealed class ConflictException : AppException
{
    public ConflictException(string message) : base(message)
    {
    }

    public override string Code => "conflict";
}

/// <summary>Failed authentication -> HTTP 401.</summary>
public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override string Code => "unauthorized";
}

/// <summary>Violation of a domain invariant (guard clause inside an entity).</summary>
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
