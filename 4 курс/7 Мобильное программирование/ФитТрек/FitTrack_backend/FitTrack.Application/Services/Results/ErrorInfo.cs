
namespace FitTrack.Application.Services.Results;

public class ErrorInfo
{
    public ErrorType Type { get; }
    public string Message { get; }
    public string? Source { get; }
    public string? StackTrace { get; }
    public DateTime Timestamp { get; }

    private ErrorInfo(ErrorType type, string message, string? source, string? stackTrace, DateTime timestamp)
    {
        Type = type;
        Message = message;
        Source = source;
        StackTrace = stackTrace;
        Timestamp = timestamp;
    }

    public ErrorInfo(ErrorType type, string message, string? source = null, string? stackTrace = null)
        : this(type, message, source, stackTrace, DateTime.UtcNow) { }
}
