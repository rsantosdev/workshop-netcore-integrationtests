using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Workshop.IntegrationTests.Api;
using Workshop.IntegrationTests.Platform.Data;

namespace Workshop.IntegrationTests.Tests.Configuration
{
    public class WorkshopTestFixture : WebApplicationFactory<Startup>
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

            SetupDatabase();
        }

        public DbContextOptions<TContext> GetDbContextOptions<TContext>() where TContext : DbContext
        {
            var connString = Configuration.GetConnectionString("DefaultConnection");
            var opts = new DbContextOptionsBuilder<TContext>();
            opts.UseSqlServer(connString, 
                provider => provider.EnableRetryOnFailure());
            opts.EnableSensitiveDataLogging();

            return opts.Options;
        }

        private void SetupDatabase()
        {
            using var context = new WorkshopDataContext(GetDbContextOptions<WorkshopDataContext>());

            var pendingMigrations = context.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                context.Database.Migrate();
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(cfg =>
            {
                cfg.Sources.Clear();
                cfg.AddConfiguration(Configuration);
            });
        }
    }
}
