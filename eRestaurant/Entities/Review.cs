namespace eRestaurant.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Text { get; set; }
        public int Rating { get; set; }

        public int? DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
