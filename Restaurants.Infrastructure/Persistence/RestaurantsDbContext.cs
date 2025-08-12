
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Restaurants.Domin.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContext : DbContext
    {
        public RestaurantsDbContext(DbContextOptions options) : base(options) { }
        public RestaurantsDbContext() { }
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .HasMany(temp=>temp.Dishes)
                .WithOne()
                .HasForeignKey(temp => temp.RestauratId);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(temp => temp.Address);
        }

    }
}
