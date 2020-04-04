using System;

namespace Workshop.IntegrationTests.Platform.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public User User { get; set; }
    }
}