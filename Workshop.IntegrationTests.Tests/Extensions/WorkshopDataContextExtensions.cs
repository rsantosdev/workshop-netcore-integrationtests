using System;
using System.Threading.Tasks;
using Workshop.IntegrationTests.Platform.Data;
using Workshop.IntegrationTests.Platform.Models;
using Workshop.IntegrationTests.Platform.Utils;

namespace Workshop.IntegrationTests.Tests.Extensions
{
    public static class WorkshopDataContextExtensions
    {
        public static async Task<User> CreateTestUser(this WorkshopDataContext context, Action<User> action = null)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = $"testuser-{Guid.NewGuid()}",
                PasswordSalt = PasswordHash.GenerateSalt()
            };
            user.Password = PasswordHash.HashPassword("test123", user.PasswordSalt);
            action?.Invoke(user);

            context.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
