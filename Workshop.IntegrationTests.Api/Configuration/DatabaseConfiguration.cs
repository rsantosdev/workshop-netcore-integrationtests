using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workshop.IntegrationTests.Platform.Data;

namespace Workshop.IntegrationTests.Api.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration, string connName = "DefaultConnection")
        {
            services.AddDbContext<WorkshopDataContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString(connName),
                    providerOptions => providerOptions.EnableRetryOnFailure());
                
                builder.EnableSensitiveDataLogging();
            });
        }
    }
}