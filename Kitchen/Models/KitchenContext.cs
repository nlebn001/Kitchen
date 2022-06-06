using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitchen.Models
{
    public class KitchenContext : DbContext
    {
        public KitchenContext(DbContextOptions<KitchenContext> options) : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PrepWay> PrepWays { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }
        public DbSet<DishIngredient> DishesIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(x => new { x.DishId, x.IngredientId });
            modelBuilder.Entity<DishIngredient>().HasOne(x => x.Dish).WithMany(x => x.DishIngredients);
            modelBuilder.Entity<DishIngredient>().HasOne(x => x.Ingredient).WithMany(x => x.DishIngredients);

            modelBuilder.Entity<Ingredient>().HasKey(x => x.Id);
            modelBuilder.Entity<Ingredient>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Ingredient>().HasOne(x => x.IngredintType).
                WithMany(x => x.Ingredients).HasForeignKey(x => x.IngredientTypeId);
            modelBuilder.Entity<Ingredient>().HasOne(x => x.StorageType).
                WithMany(x => x.Ingredients).HasForeignKey(x => x.StorageTypeId);

            modelBuilder.Entity<Dish>().HasKey(x => x.Id);
            modelBuilder.Entity<Dish>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Dish>().Property(x => x.PrepTime).IsRequired();
            modelBuilder.Entity<Dish>().Property(x => x.Temperature).IsRequired();
            modelBuilder.Entity<Dish>().HasOne(x => x.PrepWay).WithMany(x => x.Dishes)
                .HasForeignKey(x => x.PrepWayId);

            modelBuilder.Entity<PrepWay>().HasKey(x => x.Id);
        }
    }
}
