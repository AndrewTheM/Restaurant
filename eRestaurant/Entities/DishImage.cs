using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Entities
{
    public class DishImage
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public int? DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
