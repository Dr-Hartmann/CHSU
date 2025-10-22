namespace FitTrack.Application.Services.Results;

public class Result
{
    private readonly ErrorInfo? _error;

    public bool IsSuccess { get; }
    public ErrorInfo Error => !IsSuccess
        ? _error ?? throw new InvalidOperationException("Failed result does not contain information about the error")
        : throw new InvalidOperationException("Cannot access Error property on successful result");

    protected Result(bool isSuccess, ErrorInfo? error = null)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(ErrorType errorType, string message, string? source = null, string? stackTrace = null)
        => new(false, new ErrorInfo(errorType, message, source, stackTrace));
    public static Result Failure(ErrorInfo errorInfo)
        => new(false, errorInfo);

    // Common error types
    public static Result NotFound(string message = "Resource not found", string? source = null)
        => Failure(ErrorType.NotFound, message, source);

    public static Result ValidationError(string message = "Validation failed", string? source = null)
        => Failure(ErrorType.ValidationError, message, source);

    public static Result Unauthorized(string message = "Access denied", string? source = null)
        => Failure(ErrorType.Unauthorized, message, source);

    public static Result Forbidden(string message = "Access forbidden", string? source = null)
        => Failure(ErrorType.Forbidden, message, source);

    public static Result Conflict(string message = "Conflict occurred", string? source = null)
        => Failure(ErrorType.Conflict, message, source);

    public static Result InternalError(string message = "Internal server error", string? source = null)
        => Failure(ErrorType.InternalError, message, source);

    // Authentication specific errors
    public static Result InvalidCredentials(string message = "Invalid login or password", string? source = null)
        => Failure(ErrorType.InvalidCredentials, message, source);

    public static Result UserAlreadyExists(string message = "User with this login already exists", string? source = null)
        => Failure(ErrorType.UserAlreadyExists, message, source);

    public static Result InvalidToken(string message = "Invalid or expired token", string? source = null)
        => Failure(ErrorType.InvalidToken, message, source);

    public static Result UserNotFound(string message = "User not found", string? source = null)
        => Failure(ErrorType.UserNotFound, message, source);

    // Business logic errors
    public static Result InsufficientPermissions(string message = "Insufficient permissions", string? source = null)
        => Failure(ErrorType.InsufficientPermissions, message, source);

    public static Result ResourceLocked(string message = "Resource is locked", string? source = null)
        => Failure(ErrorType.ResourceLocked, message, source);

    public static Result RateLimitExceeded(string message = "Rate limit exceeded", string? source = null)
        => Failure(ErrorType.RateLimitExceeded, message, source);

    // Convert to Result<T>
    public Result<T> As<T>(T? data = default) => IsSuccess
        ? Result<T>.Success(data!)
        : Result<T>.Failure(Error.Type, Error.Message);

    // Helper methods
    public bool IsErrorType(ErrorType errorType) => !IsSuccess && Error.Type == errorType;
}

public class Result<T> : Result
{
    private readonly T? _data;

    public T Data => IsSuccess
        ? _data!
        : throw new InvalidOperationException("Cannot access data of failed result");

    protected Result(T? data, bool isSuccess, ErrorInfo? error = null)
        : base(isSuccess, error)
    {
        _data = data;
    }

    public static Result<T> Success(T data) => new(data, true);
    public static new Result<T> Failure(ErrorType errorType, string message, string? source = null, string? stackTrace = null)
        => new(default, false, new ErrorInfo(errorType, message, source, stackTrace));
    public static new Result<T> Failure(ErrorInfo errorInfo)
        => new(default, false, errorInfo);

    // Common error types (наследуем от базового класса)
    public static new Result<T> NotFound(string message = "Resource not found", string? source = null)
        => Result.NotFound(message, source).As<T>();

    public static new Result<T> ValidationError(string message = "Validation failed", string? source = null)
        => Result.ValidationError(message, source).As<T>();

    public static new Result<T> Unauthorized(string message = "Access denied", string? source = null)
        => Result.Unauthorized(message, source).As<T>();

    public static new Result<T> Forbidden(string message = "Access forbidden", string? source = null)
        => Result.Forbidden(message, source).As<T>();

    public static new Result<T> Conflict(string message = "Conflict occurred", string? source = null)
        => Result.Conflict(message, source).As<T>();

    public static new Result<T> InternalError(string message = "Internal server error", string? source = null)
        => Result.InternalError(message, source).As<T>();

    // Authentication specific errors
    public static new Result<T> InvalidCredentials(string message = "Invalid login or password", string? source = null)
        => Result.InvalidCredentials(message, source).As<T>();

    public static new Result<T> UserAlreadyExists(string message = "User with this login already exists", string? source = null)
        => Result.UserAlreadyExists(message, source).As<T>();

    public static new Result<T> InvalidToken(string message = "Invalid or expired token", string? source = null)
        => Result.InvalidToken(message, source).As<T>();

    public static new Result<T> UserNotFound(string message = "User not found", string? source = null)
        => Result.UserNotFound(message, source).As<T>();

    // Business logic errors
    public static new Result<T> InsufficientPermissions(string message = "Insufficient permissions", string? source = null)
        => Result.InsufficientPermissions(message, source).As<T>();

    public static new Result<T> ResourceLocked(string message = "Resource is locked", string? source = null)
        => Result.ResourceLocked(message, source).As<T>();

    public static new Result<T> RateLimitExceeded(string message = "Rate limit exceeded", string? source = null)
        => Result.RateLimitExceeded(message, source).As<T>();

    // Convert to another Result type
    public new Result<TNew> As<TNew>(TNew? newData = default) => new(newData, IsSuccess, Error);
}
