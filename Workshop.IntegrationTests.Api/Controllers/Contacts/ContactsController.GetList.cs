using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.IntegrationTests.Api.Extensions;
using Workshop.IntegrationTests.Platform.Data;

namespace Workshop.IntegrationTests.Api.Controllers.Contacts
{
    public partial class ContactsController
    {
        [HttpGet("/contacts")]
        public async Task<IActionResult> GetContacts(
            [FromServices]WorkshopDataContext context)
        {
            var contacts = await context.Contacts
                .Where(x => x.UserId == User.GetUserId())
                .ToListAsync();

            return Ok(contacts);
        }
    }
}
