using System.Collections.Generic;

namespace eRestaurant.API.Entities
{
    public class DishType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Dish> Dishes { get; set; }
    }
}
