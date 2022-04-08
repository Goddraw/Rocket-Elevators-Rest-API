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
        public DbSet<Building> buildings { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Intervention> interventions { get; set; }

    }
}