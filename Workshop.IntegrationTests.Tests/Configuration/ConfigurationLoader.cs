using System;
using Microsoft.Extensions.Configuration;

namespace Workshop.IntegrationTests.Tests.Configuration
{
    public static class ConfigurationLoader
    {
        public static Lazy<IConfiguration> Instance { get; } = new Lazy<IConfiguration>(BuildConfiguration);

        static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.CI.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
