using Microsoft.Extensions.Logging;

namespace FitTrack.DataAccess.LoggerProviders;

internal class ConsoleLoggerProvider(string filePath = "log.txt") : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new MyLogger(filePath);

    private class MyLogger(string filePath) : ILogger
    {
        IDisposable? ILogger.BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            File.AppendAllText(filePath, formatter(state, exception));
            Console.WriteLine(formatter(state, exception));
        }
    }

    public void Dispose() { }
}
