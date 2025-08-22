
using Microsoft.Extensions.Logging;

namespace backend.Tests.Common;

/// <summary>
/// テストプロジェクトで用いるロガークラス
/// 標準出力にログの内容を出力する
/// </summary>
/// <typeparam name="T"></typeparam>
public class LoggerForTest<T> : ILogger<T>
{
    public LoggerForTest()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    IDisposable? ILogger.BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        var output = $"[{logLevel}] {typeof(T).Name}: {message}";
        if (exception != null)
        {
            output += $" Exception: {exception}";
        }
        // 標準出力
        Console.WriteLine(output);
    }
}