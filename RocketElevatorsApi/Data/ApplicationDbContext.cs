using Microsoft.EntityFrameworkCore;
using RocketElevatorsApi.Models;

namespace RocketElevatorsApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {

        }

        public DbSet<Battery> batteries { get; set; }
        public DbSet<Elevator> elevators { get; set; }
    }
}