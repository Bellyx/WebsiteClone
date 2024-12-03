using Microsoft.EntityFrameworkCore;
using WebsiteClone.Models;

namespace WebsiteClone.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            // Here, the properties are not directly initialized,
            // but the context will handle their initialization.
        }
        public DbSet<Discordpermiss> Discordpermiss { get; set; }
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Car_plate> Car_Plate { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<Rental> Rental { get; set; } = null!;
        

    }
}
