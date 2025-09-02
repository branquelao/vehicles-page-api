using Microsoft.EntityFrameworkCore;
using VehiclesPageAPI.Models;

namespace VehiclesPageAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
