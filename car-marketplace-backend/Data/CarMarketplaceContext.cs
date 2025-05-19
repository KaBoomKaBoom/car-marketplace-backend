using car_marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace car_marketplace_backend.Data
{
    public class CarMarketplaceContext : DbContext
    {
        public CarMarketplaceContext(DbContextOptions<CarMarketplaceContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            // Configure the User entity
            modelBuilder.Entity<User>()
                .ToTable("Users", "dbo")
                .HasIndex(u => u.Id)
                .IsUnique();
        }
    }
}
