
namespace Kitchen.Models
{
    public interface IDishRepository : IDisposable
    {
        Task<IEnumerable<Dish>> GetDishesAsync();
        Task<Dish> GetDishAsync(int id);
        Task CreateDishAsync(Dish dish);
        Task DeleteDishAsync(int id);
        Task UpdateDishAsync(int id, Dish dish);
        Task SaveDishAsync();

    }
}