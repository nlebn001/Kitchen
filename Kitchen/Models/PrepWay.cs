namespace Kitchen.Models
{
    public class PrepWay
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Dish> Dishes { get; set; } = new();
    }
}
