using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Workshop.IntegrationTests.Platform.Data;

namespace Workshop.IntegrationTests.Tests.Configuration
{
    public static class DbContextFactory
    {
        public static WorkshopDataContext CreateDataContext(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            var opts = new DbContextOptionsBuilder<WorkshopDataContext>();
            opts.UseSqlServer(connString,
                provider => provider.EnableRetryOnFailure());
            opts.EnableSensitiveDataLogging();

            return new WorkshopDataContext(opts.Options);
        }
    }
}
