using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workshop.IntegrationTests.Api.Controllers.Ping
{
    public partial class PingController
    {
        [HttpGet("ping"), AllowAnonymous]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}
