using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Services
{
    public class DataGenerator
    {
        public static void Initialize(KitchenContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();

                // Look for any data.
                if (context.Dishes.Any()
                    && context.Ingredients.Any()
                    && context.IngredientTypes.Any()
                    && context.PrepWays.Any()
                    && context.StorageTypes.Any()
                    && context.DishesIngredients.Any())
                {
                    return;   // Data was already seeded
                }
                // INGREDINET TYPES
                #region IngredientTypes
                context.IngredientTypes.AddRange(
                           new IngredientType { Id = 1, Name = "Meat" },
                           new IngredientType { Id = 2, Name = "Fish" },
                           new IngredientType { Id = 3, Name = "Groats" },
                           new IngredientType { Id = 4, Name = "Oil" });
                #endregion

                // PREP WAYS
                #region PrepWays
                context.PrepWays.AddRange(
                           new PrepWay { Id = 1, Name = "Frying" },
                           new PrepWay { Id = 2, Name = "Boiling" },
                           new PrepWay { Id = 3, Name = "Quenching" });
                #endregion

                //STORAGE TYPES
                #region StorageTypes
                context.StorageTypes.AddRange(
                           new StorageType { Id = 1, Name = "Freezer" },
                           new StorageType { Id = 2, Name = "Fridge" },
                           new StorageType { Id = 3, Name = "Storage room" });
                #endregion

                //INGREDIENTS
                #region Ingredients
                context.Ingredients.AddRange(
                           new Ingredient
                           {
                               Id = 1,
                               Name = "Chicken",
                               IngredientTypeId = 1,
                               StorageTypeId = 2
                           },
                           new Ingredient
                           {
                               Id = 2,
                               Name = "Salmon",
                               IngredientTypeId = 2,
                               StorageTypeId = 2
                           },
                           new Ingredient
                           {
                               Id = 3,
                               Name = "Rice",
                               IngredientTypeId = 3,
                               StorageTypeId = 3
                           },
                           new Ingredient
                           {
                               Id = 4,
                               Name = "Sunflower oil",
                               IngredientTypeId = 4,
                               StorageTypeId = 3
                           });

                #endregion

                //DISHES
                #region Dishes
                context.Dishes.AddRange(
                    new Dish
                    {
                        Id = 1,
                        Name = "Chicken with rice",
                        PrepTime = new TimeSpan(0, 15, 0),
                        PrepWayId = 1,
                        Temperature = 80,
                    });
                #endregion

                context.DishesIngredients.AddRange(
                    new DishIngredient { DishId = 1, IngredientId = 1 },
                    new DishIngredient { DishId = 1, IngredientId = 3 }
                    );

                context.SaveChanges();
            }
        }
    }
}
