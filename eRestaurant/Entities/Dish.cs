using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Entities
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double PortionSize { get; set; }
        public TimeSpan? CookingTime { get; set; }

        public int? TypeId { get; set; }
        public DishType Type { get; set; }

        public int? UnitId { get; set; }
        public UnitOfMeasurement PortionUnit { get; set; }

        public IList<DishImage> Images { get; set; }

        public IList<OrderDish> OrderDishes { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}
