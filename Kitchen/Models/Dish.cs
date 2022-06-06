namespace Kitchen.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan PrepTime { get; set; }
        public double? Temperature { get; set; }
        public int PrepWayId { get; set; }
        public PrepWay? PrepWay { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new();
    }
}
