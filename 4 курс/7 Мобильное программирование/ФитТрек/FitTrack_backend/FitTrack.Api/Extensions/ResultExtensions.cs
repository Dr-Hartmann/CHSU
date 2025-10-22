using FitTrack.Application.Services.Results;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Расширения для преобразования объектов Result в IActionResult
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Преобразует Result{T} в соответствующий IActionResult
    /// </summary>
    /// <typeparam name="T">Тип данных результата</typeparam>
    /// <param name="result">Результат операции</param>
    /// <param name="context">HTTP контекст (опционально)</param>
    /// <returns>IActionResult с данными или ошибкой</returns>
    public static IActionResult ToActionResult<T>(this Result<T> result, HttpContext? context = null)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Data);
        }

        return CreateProblemDetailsResult(result.Error, context);
    }

    /// <summary>
    /// Преобразует Result в соответствующий IActionResult
    /// </summary>
    /// <param name="result">Результат операции без данных</param>
    /// <param name="context">HTTP контекст (опционально)</param>
    /// <returns>IActionResult с статусом или ошибкой</returns>
    public static IActionResult ToActionResult(this Result result, HttpContext? context = null)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return CreateProblemDetailsResult(result.Error, context);
    }

    /// <summary>
    /// Создает детализированный ответ с ошибкой в формате ProblemDetails
    /// </summary>
    /// <param name="error">Информация об ошибке</param>
    /// <param name="context">HTTP контекст</param>
    /// <returns>IActionResult с детализированной ошибкой</returns>
    private static IActionResult CreateProblemDetailsResult(ErrorInfo? error, HttpContext? context = null)
    {
        if (error == null)
        {
            return new ObjectResult(new ProblemDetails
            {
                Title = "Unknown Error",
                Status = StatusCodes.Status500InternalServerError,
                Instance = context?.Request.Path ?? string.Empty
            })
            { StatusCode = StatusCodes.Status500InternalServerError };
        }

        var problemDetails = new ProblemDetails
        {
            Title = GetTitleForErrorType(error.Type),
            Detail = error.Message,
            Status = GetStatusCodeForErrorType(error.Type),
            Instance = context?.Request.Path ?? string.Empty
        };

        // Добавляем дополнительную информацию
        problemDetails.Extensions["errorType"] = error.Type.ToString();
        problemDetails.Extensions["timestamp"] = error.Timestamp.ToString("O");

        if (!string.IsNullOrEmpty(error.Source))
        {
            problemDetails.Extensions["source"] = error.Source;
        }

        if (!string.IsNullOrEmpty(error.StackTrace))
        {
            problemDetails.Extensions["stackTrace"] = error.StackTrace;
        }

        return problemDetails.Status switch
        {
            StatusCodes.Status400BadRequest => new BadRequestObjectResult(problemDetails),
            StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(problemDetails),
            StatusCodes.Status403Forbidden => new ObjectResult(problemDetails) { StatusCode = StatusCodes.Status403Forbidden },
            StatusCodes.Status404NotFound => new NotFoundObjectResult(problemDetails),
            StatusCodes.Status409Conflict => new ConflictObjectResult(problemDetails),
            StatusCodes.Status429TooManyRequests => new ObjectResult(problemDetails) { StatusCode = StatusCodes.Status429TooManyRequests },
            _ => new ObjectResult(problemDetails) { StatusCode = problemDetails.Status }
        };
    }

    /// <summary>
    /// Определяет HTTP статус код на основе типа ошибки
    /// </summary>
    /// <param name="errorType">Тип ошибки</param>
    /// <returns>Соответствующий HTTP статус код</returns>
    private static int GetStatusCodeForErrorType(ErrorType errorType) => errorType switch
    {
        ErrorType.NotFound or ErrorType.UserNotFound => StatusCodes.Status404NotFound,
        ErrorType.ValidationError => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized or ErrorType.InvalidCredentials or ErrorType.InvalidToken => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden or ErrorType.InsufficientPermissions => StatusCodes.Status403Forbidden,
        ErrorType.Conflict or ErrorType.UserAlreadyExists => StatusCodes.Status409Conflict,
        ErrorType.RateLimitExceeded => StatusCodes.Status429TooManyRequests,
        ErrorType.ResourceLocked => StatusCodes.Status423Locked,
        ErrorType.InternalError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    /// <summary>
    /// Определяет заголовок ошибки для пользователя на основе типа ошибки
    /// </summary>
    /// <param name="errorType">Тип ошибки</param>
    /// <returns>Человеко-читаемый заголовок ошибки</returns>
    private static string GetTitleForErrorType(ErrorType errorType) => errorType switch
    {
        ErrorType.NotFound => "Resource Not Found",
        ErrorType.UserNotFound => "User Not Found",
        ErrorType.ValidationError => "Validation Error",
        ErrorType.Unauthorized => "Unauthorized Access",
        ErrorType.Forbidden => "Access Forbidden",
        ErrorType.InvalidCredentials => "Invalid Credentials",
        ErrorType.InvalidToken => "Invalid Token",
        ErrorType.Conflict => "Resource Conflict",
        ErrorType.UserAlreadyExists => "User Already Exists",
        ErrorType.InsufficientPermissions => "Insufficient Permissions",
        ErrorType.ResourceLocked => "Resource Locked",
        ErrorType.RateLimitExceeded => "Rate Limit Exceeded",
        ErrorType.InternalError => "Internal Server Error",
        _ => "Bad Request"
    };
}
