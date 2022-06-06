using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Kitchen.Models
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly KitchenContext _context;
        private readonly ILogger _logger;

        IIncludableQueryable<Ingredient, StorageType?> _fullIngredients;

        private bool _disposed = false;

        public IngredientRepository(KitchenContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;

            _fullIngredients = _context.Ingredients
                 .Include(u => u.IngredintType)
                 .Include(u => u.StorageType);

        }


        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await Task.FromResult(_fullIngredients);
        }

        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            Ingredient? ingredients = await _fullIngredients.SingleOrDefaultAsync(u => u.Id == id);
            return await Task.FromResult(ingredients);
        }

        public async Task CreateIngredientAsync(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
            await Task.CompletedTask;
        }
        public async Task UpdateIngredientAsync(int id, Ingredient ingredient)
        {
            Ingredient dbIngredient = await _context.Ingredients.FindAsync(id);
            if (dbIngredient == null) return;
            dbIngredient = ingredient;
            await Task.CompletedTask;
        }
        public async Task DeleteIngredientAsync(int id)
        {
            Ingredient dbIngredient = await _context.Ingredients.FindAsync(id);
            if (dbIngredient == null) return;
            _context.Remove(dbIngredient);
            await Task.CompletedTask;
        }

        public async Task SaveIngredientAsync() => await _context.SaveChangesAsync();

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
