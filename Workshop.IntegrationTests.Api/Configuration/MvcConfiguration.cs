using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Workshop.IntegrationTests.Api.Configuration
{
    public static class MvcConfiguration
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                // Forces all controller actions to be authenticated by default,
                // unless they have AllowAnonymous attribute
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
    }
}
