using Microsoft.Extensions.Logging;

namespace Functions.Tests;

public class ListLogger : ILogger
{
    public IList<string> Logs;

    public ListLogger()
    {
        Logs = new List<string>();
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return NullScope.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    public void Log<TState>(LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        var message = formatter(state, exception);
        Logs.Add(message);
    }
}