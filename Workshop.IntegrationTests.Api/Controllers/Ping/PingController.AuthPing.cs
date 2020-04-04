using Microsoft.AspNetCore.Mvc;
using Workshop.IntegrationTests.Api.Extensions;

namespace Workshop.IntegrationTests.Api.Controllers.Ping
{
    public partial class PingController
    {
        [HttpGet("auth-ping")]
        public IActionResult AuthPing()
        {
            return Ok($"pong {User.GetUserId()}");
        }
    }
}
