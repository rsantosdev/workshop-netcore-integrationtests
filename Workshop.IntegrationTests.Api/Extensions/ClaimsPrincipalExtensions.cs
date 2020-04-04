using System;
using System.Linq;
using System.Security.Claims;

namespace Workshop.IntegrationTests.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userClaim = user?.Claims.FirstOrDefault(x => x.Type == "user.id");
            Guid.TryParse(userClaim?.Value, out var id);
            return id;
        }
    }
}
