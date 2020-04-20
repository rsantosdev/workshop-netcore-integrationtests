using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Workshop.IntegrationTests.Api;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Configuration
{
    public class WorkshopTestFixture : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        public readonly IConfiguration Configuration;
        public readonly ILogger Logger;
        public readonly string SqlDeleteAllTables;
        
        public WorkshopTestFixture()
        {
            Configuration = ConfigurationLoader.Instance.Value;
            Logger = TestLoggerFactory.Instance.Value;

            SqlDeleteAllTables = File.ReadAllText(
                Path.Combine(Directory.GetCurrentDirectory(), "SqlScripts", "deletealltables.sql"));
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(cfg =>
            {
                cfg.Sources.Clear();
                cfg.AddConfiguration(Configuration);
            });
        }

        public async Task InitializeAsync()
        {
            await using var context = DbContextFactory.CreateDataContext(Configuration);

            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }

            //TODO: create any Table, View or SP that is not in Migrations
        }

        public async Task DisposeAsync()
        {
            await Task.Yield();
        }
    }
}
