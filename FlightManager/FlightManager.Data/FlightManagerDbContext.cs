using Microsoft.EntityFrameworkCore;
using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;

namespace FlightManager.Data
{
    public class FlightManagerDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }

        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options) : base(options)
        {

        }
        public FlightManagerDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    PhoneNumber = "1234567890",
                    Password = "admin123", 
                    PersonalId = "1",
                    Address = "123 Admin St.",
                    RoleId = (int)Role.Admin
                });
        }
    }
}
