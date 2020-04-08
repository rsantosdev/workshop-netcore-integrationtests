using Workshop.IntegrationTests.Platform.Utils;
using Xunit;

namespace Workshop.IntegrationTests.Tests.ServiceTests
{
    public class PasswordHashTests
    {
        [Fact]
        public void HashPasswordShouldReturnEmptyWhenEmptyPassword()
        {
            var result = PasswordHash.HashPassword(string.Empty, string.Empty);
            Assert.Equal(string.Empty, result);
        }
    }
}
