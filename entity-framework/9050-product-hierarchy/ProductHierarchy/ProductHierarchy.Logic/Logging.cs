using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace ProductHierarchy.Logic
{
    public class DebugLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new DebugLogger();
        }

        public void Dispose() { }

        /// <summary>
        /// Implements a logger that write to the debugger's output window
        /// </summary>
        private class DebugLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                Debug.WriteLine(formatter(state, exception));
            }

            public IDisposable BeginScope<TState>(TState state) => null;
        }
    }
}
