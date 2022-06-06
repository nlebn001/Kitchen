using Kitchen.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {

        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger _logger;

        public IngredientsController(IIngredientRepository ingredientRepository, ILogger logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            return Ok(await _ingredientRepository.GetIngredientsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            return Ok(await _ingredientRepository.GetIngredientAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
        {
            await _ingredientRepository.CreateIngredientAsync(ingredient);
            await _ingredientRepository.SaveIngredientAsync();
            return Ok(ingredient);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] Ingredient ingredient)
        {
            await _ingredientRepository.UpdateIngredientAsync(id, ingredient);
            await _ingredientRepository.SaveIngredientAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientRepository.DeleteIngredientAsync(id);
            await _ingredientRepository.SaveIngredientAsync();
            return NoContent();
        }
    }
}
