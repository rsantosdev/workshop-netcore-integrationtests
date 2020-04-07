using System;
using Microsoft.Extensions.Logging;

namespace Workshop.IntegrationTests.Tests.Configuration
{
    public static class TestLoggerFactory
    {
        public static Lazy<ILogger> Instance { get; } = new Lazy<ILogger>(BuildLogger);

        static ILogger BuildLogger()
        {
            var factory = LoggerFactory.Create(c =>
            {
                c.AddFilter("Microsoft", LogLevel.Debug)
                    .AddFilter("Default", LogLevel.Debug)
                    .AddFilter("System", LogLevel.Debug)
                    .AddConsole()
                    .AddEventSourceLogger()
                    .AddDebug();
            });

            return factory.CreateLogger("Integration Tests");
        }
    }
}
