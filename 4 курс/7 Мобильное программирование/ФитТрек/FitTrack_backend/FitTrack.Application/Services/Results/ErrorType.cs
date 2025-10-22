
namespace FitTrack.Application.Services.Results;

public enum ErrorType
{
    // Common errors
    NotFound,
    ValidationError,
    Unauthorized,
    Forbidden,
    Conflict,
    InternalError,

    // Authentication specific errors
    InvalidCredentials,
    UserAlreadyExists,
    InvalidToken,
    UserNotFound,

    // Business logic errors
    InsufficientPermissions,
    ResourceLocked,
    RateLimitExceeded
}
