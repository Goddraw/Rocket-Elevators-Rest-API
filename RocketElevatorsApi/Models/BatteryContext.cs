using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorsApi.Models
{
    public class BatteryContext : DbContext
    {
        public BatteryContext(DbContextOptions<BatteryContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> Battery { get; set; } = null!;
    }
}