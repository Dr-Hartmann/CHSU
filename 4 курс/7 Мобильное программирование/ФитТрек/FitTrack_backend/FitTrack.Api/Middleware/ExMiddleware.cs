using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FitTrack.Api.Middleware;

/// <summary>
/// Middleware для глобальной обработки исключений в приложении
/// </summary>
public class ExMiddleware(RequestDelegate next, ILogger<ExMiddleware> logger)
{
    /// <summary>
    /// Обрабатывает HTTP запрос и перехватывает исключения
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                NotImplementedException => StatusCodes.Status501NotImplemented,

                // EF Core специфичные исключения
                DbUpdateConcurrencyException => StatusCodes.Status409Conflict,
                DbUpdateException => StatusCodes.Status409Conflict,

                // Отмена операции
                OperationCanceledException => StatusCodes.Status499ClientClosedRequest,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitleForStatus(statusCode),
                Detail = context.RequestServices
                    .GetRequiredService<IHostEnvironment>()
                    .IsDevelopment()
                        ? ex.Message
                        : "An error occurred while processing your request",
                Instance = context.Request.Path,
                Extensions =
            {
                ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier
            }
            };

            // В development добавляем stack trace
            if (context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                problem.Extensions["exception"] = ex.GetType().Name;
                problem.Extensions["stackTrace"] = ex.StackTrace;
            }

            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    /// <summary>
    /// Возвращает заголовок для указанного HTTP статус кода
    /// </summary>
    private static string GetTitleForStatus(int status) => status switch
    {
        StatusCodes.Status400BadRequest => "Bad Request",
        StatusCodes.Status401Unauthorized => "Unauthorized",
        StatusCodes.Status404NotFound => "Not Found",
        StatusCodes.Status409Conflict => "Conflict",
        StatusCodes.Status499ClientClosedRequest => "Client Closed Request",
        StatusCodes.Status500InternalServerError => "Internal Server Error",
        StatusCodes.Status501NotImplemented => "Not Implemented",
        _ => "Error"
    };
}
