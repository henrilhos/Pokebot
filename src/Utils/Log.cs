using Pokebot.Models;
using System;

namespace Pokebot.Utils
{
    public static class Log
    {
        public delegate void LogEventHandler(LogEventArgs e);

        public static event LogEventHandler? LogReceived;

        public static void Debug(string message)
        {
            Send(LogLevel.Debug, message);
        }

        public static void Info(string message)
        {
            Send(LogLevel.Info, message);
        }

        public static void Warn(string message)
        {
            Send(LogLevel.Warn, message);
        }

        public static void Error(string message)
        {
            Send(LogLevel.Error, message);
        }

        public static void Error(Exception exception)
        {
            Send(LogLevel.Error, exception.Message, exception);
        }

        public static void Fatal(string message)
        {
            Send(LogLevel.Fatal, message);
        }

        public static void Send(LogLevel level, string message, Exception? exception = null)
        {
            LogReceived?.Invoke(new LogEventArgs(level, message, exception));
        }
    }

    public record LogEventArgs(LogLevel Level, string Message, Exception? Exception = null) { }
}
