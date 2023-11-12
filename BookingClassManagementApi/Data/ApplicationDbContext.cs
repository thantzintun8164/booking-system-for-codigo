using BookingClassManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingClassManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Schdule> Schdules { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PurchasePackage> PurchasePackages { get; set; }
        public DbSet<UserPaymentCard> UserPaymentCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }
        //Added Seed data
        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country { Id = 1, CountryName = "SG", CountryCode = "+65"},
                new Country { Id = 2, CountryName = "MYN", CountryCode = "+95" }
            );
            builder.Entity<Class>().HasData(
                new Class { Id = 1, ClassName = "Yoga Class", CountryId = 1 },
                new Class { Id = 2, ClassName = "Violin Class", CountryId = 2 }
            );
            builder.Entity<Package>().HasData(
                new Package { Id = 1, Name = "1 hr Yoga Class", Description = "1 hr Yoga Class Desc", ClassId = 1, Price = 500, Currency ="SGD", CreditCost = 5, ExpireDate = DateTime.Now.AddDays(5)},
                new Package { Id = 2, Name = "3 hr Violin Class", Description = "1 hr Yoga Class Desc", ClassId = 2, Price = 500, Currency = "SGD", CreditCost = 5, ExpireDate = DateTime.Now.AddDays(5) }
            );
            
            
        }
    }
}
