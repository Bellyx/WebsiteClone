using Microsoft.EntityFrameworkCore;
using WebsiteClone.Models;

namespace WebsiteClone.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) { 
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Car_plate> Car_Plate { get; set; }
    }
}
