using Microsoft.EntityFrameworkCore;
using Workshop.IntegrationTests.Platform.Models;

namespace Workshop.IntegrationTests.Platform.Data
{
    public class WorkshopDataContext : DbContext
    {
        public WorkshopDataContext(DbContextOptions<WorkshopDataContext> options) : base(options)
        {
        }
        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.User)
                    .WithMany()
                    .IsRequired();
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Email).IsRequired();
                e.Property(p => p.Password).IsRequired();
                e.Property(p => p.PasswordSalt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
