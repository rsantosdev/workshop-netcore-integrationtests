using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Workshop.IntegrationTests.Platform.Models;
using Workshop.IntegrationTests.Tests.Extensions;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Controllers.Contacts
{
    public partial class ContactsControllerTests
    {
        [Fact]
        public async Task GetListShouldReturnEmptyResults()
        {
            var result = await ApiClient.GetAsync("/contacts");
            result.EnsureSuccessStatusCode();

            var contacts = await result.Content.ReadAsAsync<IEnumerable<Contact>>();
            Assert.Empty(contacts);
        }

        [Fact]
        public async Task GetListShouldReturnCurrentUserContacts()
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = "Test Contact",
                Email = "contact@wokrshop.com",
                UserId = CurrentUser.Id
            };
            DataContext.Contacts.Add(contact);
            await DataContext.SaveChangesAsync();

            var result = await ApiClient.GetAsync("/contacts");
            result.EnsureSuccessStatusCode();

            var contacts = await result.Content.ReadAsAsync<List<Contact>>();
            Assert.Single(contacts);
            Assert.Equal(contact.Id, contacts.FirstOrDefault()?.Id);
        }

        [Fact]
        public async Task GetListShouldReturnContactsOnlyFromCurrentUser()
        {
            var user = await DataContext.CreateTestUser();

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = "Test Contact",
                Email = "contact@wokrshop.com",
                UserId = CurrentUser.Id
            };
            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                Name = "Test Contact 2",
                Email = "contact@wokrshop.com",
                UserId = user.Id
            };

            DataContext.Contacts.AddRange(contact, contact2);
            await DataContext.SaveChangesAsync();

            var result = await ApiClient.GetAsync("/contacts");
            result.EnsureSuccessStatusCode();

            var contacts = await result.Content.ReadAsAsync<List<Contact>>();
            Assert.Single(contacts);
            Assert.Equal(contact.Id, contacts.FirstOrDefault()?.Id);
        }
    }
}
