using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Kitchen.Models
{
    public class DishRepository : IDishRepository
    {
        private readonly KitchenContext _context;
        private readonly ILogger _logger;

        IIncludableQueryable<Dish, Ingredient?> _fullDishes;

        private bool _disposed = false;

        public DishRepository(KitchenContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;

            _fullDishes = _context.Dishes
                .Include(x => x.DishIngredients).ThenInclude(x => x.Ingredient);

        }

        public async Task<IEnumerable<Dish>> GetDishesAsync()
        {
            return await Task.FromResult(_fullDishes);
        }

        public async Task<Dish> GetDishAsync(int id)
        {
            Dish? dish = await _fullDishes.SingleOrDefaultAsync(u => u.Id == id);
            return await Task.FromResult(dish);
        }

        public async Task CreateDishAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await Task.CompletedTask;
        }
        public async Task UpdateDishAsync(int id, Dish dish)
        {
            Dish dbDish = await _context.Dishes.FindAsync(id);
            if (dbDish == null) return;
            dbDish = dish;
            await Task.CompletedTask;
        }
        public async Task DeleteDishAsync(int id)
        {
            Dish dbDish = await _context.Dishes.FindAsync(id);
            if (dbDish == null) return;
            _context.Remove(dbDish);
            await Task.CompletedTask;
        }

        public async Task SaveDishAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
