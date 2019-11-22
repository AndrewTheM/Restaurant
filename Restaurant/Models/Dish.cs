using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? TypeId { get; set; }
        public DishType Type { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int? UnitId { get; set; }
        public Unit Unit { get; set; }

        public TimeSpan? CookingTime { get; set; }
        public byte[] Image { get; set; }
    }
}
