using System;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Shared.DTO
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must not contain less than 2 or more than 50 characters.")]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must not be negative or exceed $10 thousand.")]
        public double Price { get; set; }

        [Required]
        public string TypeId { get; set; } = "1";

        public string UnitId { get; set; } = "1";

        [Range(0.1, 1000, ErrorMessage = "Portion size must not be less than 0.1 or exceed 1 thousand. Try using another unit of measurement.")]
        public double PortionSize { get; set; } = 1;

        [Range(0, 1000, ErrorMessage = "Minutes must not be negative or exceed 1 thousand.")]
        public double? CookingTime { get; set; }
    }
}
