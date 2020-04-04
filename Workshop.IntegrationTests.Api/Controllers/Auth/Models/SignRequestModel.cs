using System.ComponentModel.DataAnnotations;

namespace Workshop.IntegrationTests.Api.Controllers.Auth.Models
{
    public class SignRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
