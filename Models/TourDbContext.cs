using Microsoft.EntityFrameworkCore;

namespace kurs.Models
{
    public class TourDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public TourDbContext(DbContextOptions<TourDbContext> options) : base(options)
        {
            
        }
    }
}