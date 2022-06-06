
namespace Kitchen.Models
{
    public interface IIngredientRepository : IDisposable
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<Ingredient> GetIngredientAsync(int id);
        Task CreateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(int id, Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
        Task SaveIngredientAsync();
    }
}