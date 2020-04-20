using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Workshop.IntegrationTests.Platform.Data;
using Workshop.IntegrationTests.Platform.Jwt;
using Workshop.IntegrationTests.Platform.Models;
using Workshop.IntegrationTests.Platform.Utils;
using Workshop.IntegrationTests.Tests.Configuration;
using Xunit;

namespace Workshop.IntegrationTests.Tests.Controllers
{
    public abstract class WorkshopBaseTestController : IClassFixture<WorkshopTestFixture>, IAsyncLifetime
    {
        protected WorkshopBaseTestController(WorkshopTestFixture fixture)
        {
            Fixture = fixture;
            ApiClient = fixture.CreateClient();
        }

        public HttpClient ApiClient { get; }
        public WorkshopTestFixture Fixture { get; }
        public WorkshopDataContext DataContext { get; private set; }
        public User CurrentUser { get; private set; }
        
        public virtual async Task InitializeAsync()
        {
            // Clear Database
            // using Dapper
            //await using var con = new SqlConnection(Fixture.Configuration.GetConnectionString("DefaultConnection"));
            //await con.ExecuteAsync(Fixture.SqlDeleteAllTables);

            DataContext = DbContextFactory.CreateDataContext(Fixture.Configuration);
            await DataContext.Database.ExecuteSqlRawAsync(Fixture.SqlDeleteAllTables);

            CurrentUser = new User
            {
                Id = Guid.Parse("07F4F81F-2784-4154-BC3B-59A320BB56EB"),
                Email = "testuser@workshop.com",
                PasswordSalt = PasswordHash.GenerateSalt()
            };
            CurrentUser.Password = PasswordHash.HashPassword("test123", CurrentUser.PasswordSalt);

            DataContext.Users.Add(CurrentUser);
            await DataContext.SaveChangesAsync();

            var tokenUser = JwtHelper.GenerateJwt(CurrentUser);
            ApiClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", tokenUser.AccessToken);
        }

        public async Task DisposeAsync()
        {
            await DataContext.DisposeAsync();
        }
    }
}
