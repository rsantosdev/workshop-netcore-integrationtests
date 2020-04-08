using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Workshop.IntegrationTests.Api.Controllers.Auth.Models;
using Workshop.IntegrationTests.Platform.Jwt;
using Workshop.IntegrationTests.Tests.Extensions;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Controllers.Auth
{
    public partial class AuthControllerTests
    {
        [Fact]
        public async Task SignInShouldReturnBadRequestWithoutModel()
        {
            var response = await ApiClient.AsAnonymous().PostAsJsonAsync("/signin", new { });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignInShouldReturnBadRequestWithInvalidEmail()
        {
            var model = new SignRequestModel
            {
                Email = "xpto@workshop.com",
                Password = "123456"
            };

            var response = await ApiClient.AsAnonymous().PostAsJsonAsync("/signin", model);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignInShouldReturnBadRequestWithInvalidPassword()
        {
            var model = new SignRequestModel
            {
                Email = CurrentUser.Email,
                Password = "123456"
            };

            var response = await ApiClient.AsAnonymous().PostAsJsonAsync("/signin", model);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignInShouldReturnOk()
        {
            var model = new SignRequestModel
            {
                Email = CurrentUser.Email,
                Password = "test123"
            };

            var response = await ApiClient.AsAnonymous().PostAsJsonAsync("/signin", model);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadAsAsync<UserJwt>();
            Assert.Equal(CurrentUser.Id, user.UserId);
        }
    }
}
