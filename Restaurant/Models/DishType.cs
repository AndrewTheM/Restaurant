using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class DishType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Dish> Dishes { get; set; }
    }
}
