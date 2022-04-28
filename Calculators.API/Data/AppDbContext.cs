using Microsoft.EntityFrameworkCore;
using Calculators.API.Controllers;
using Calculators.API;

namespace Calculators.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle>? Vehicles { get; set; }
    }
}