namespace Kitchen.Models
{
    public class Ingredient
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IngredientTypeId { get; set; }
        public IngredientType? IngredintType { get; set; }
        public int StorageTypeId { get; set; }
        public StorageType? StorageType { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new();
    }
}
