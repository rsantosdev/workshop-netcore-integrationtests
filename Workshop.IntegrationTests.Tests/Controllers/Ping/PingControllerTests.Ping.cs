using System.Threading.Tasks;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Controllers.Ping
{
    public partial class PingControllerTests
    {
        [Fact]
        public async Task ShouldReturnPong()
        {
            // Act
            var result = await ApiClient.GetStringAsync("/ping");

            // Assert
            Assert.Equal("pong", result);
        }
    }
}
