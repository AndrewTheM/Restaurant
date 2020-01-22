namespace eRestaurant.API.Entities
{
    public class CartDish
    {
        public int CartId { get; set; }
        public OrderCart Cart { get; set; }

        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public int Quantity { get; set; }
    }
}
