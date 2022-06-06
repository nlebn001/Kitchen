namespace Kitchen.Models
{
    public class IngredientType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
