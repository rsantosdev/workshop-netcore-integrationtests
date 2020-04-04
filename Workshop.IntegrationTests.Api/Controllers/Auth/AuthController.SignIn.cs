using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workshop.IntegrationTests.Api.Controllers.Auth.Models;
using Workshop.IntegrationTests.Platform.Data;
using Workshop.IntegrationTests.Platform.Jwt;
using Workshop.IntegrationTests.Platform.Utils;

namespace Workshop.IntegrationTests.Api.Controllers.Auth
{
    public partial class AuthController
    {
        [HttpPost("/signin"), AllowAnonymous]
        public async Task<IActionResult> SignIn(
            [FromBody]SignRequestModel model,
            [FromServices]WorkshopDataContext context,
            [FromServices]ILogger<AuthController> logger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                logger.LogInformation($"User '{model.Email}' - Email not found");
                return BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "You're not registered, please create an account"
                });
            }

            if (PasswordHash.HashPassword(model.Password, user.PasswordSalt) != user.Password)
            {
                logger.LogInformation($"User '{user.Id}' - Password mismatch");
                return BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Invalid email or password"
                });
            }

            return Ok(JwtHelper.GenerateJwt(user));
        }
    }
}
