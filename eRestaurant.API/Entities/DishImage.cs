namespace eRestaurant.API.Entities
{
    public class DishImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public int DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
