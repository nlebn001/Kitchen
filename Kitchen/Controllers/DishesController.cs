using Kitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace Kitchen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;
        private readonly ILogger _logger;
        public DishesController(IDishRepository dishRepository, ILogger logger)
        {
            _dishRepository = dishRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDishes()
        {
            var a = await _dishRepository.GetDishesAsync();
            return Ok(a.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            return Ok(await _dishRepository.GetDishAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromBody] Dish dish)
        {
            await _dishRepository.CreateDishAsync(dish);
            await _dishRepository.SaveDishAsync();
            return Ok(dish);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] Dish dish)
        {
            await _dishRepository.UpdateDishAsync(id, dish);
            await _dishRepository.SaveDishAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            await _dishRepository.DeleteDishAsync(id);
            await _dishRepository.SaveDishAsync();
            return NoContent();
        }
    }
}
