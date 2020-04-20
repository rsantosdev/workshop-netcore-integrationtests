using System.Net;
using System.Threading.Tasks;
using Workshop.IntegrationTests.Tests.Extensions;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Controllers.Ping
{
    public partial class PingControllerTests
    {
        [Fact]
        public async Task ShouldReturn401WithoutTokenPong()
        {
            // Act
            var result = await ApiClient.AsAnonymous()
                .GetAsync("/auth-ping");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPongWithCurrentUser()
        {
            var result = await ApiClient.GetAsync("/auth-ping");
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal($"pong {CurrentUser.Id}", content);
        }
    }
}
