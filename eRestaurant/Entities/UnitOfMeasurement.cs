using System.Collections.Generic;

namespace eRestaurant.Entities
{
    public class UnitOfMeasurement
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Dish> Dishes { get; set; }
    }
}
