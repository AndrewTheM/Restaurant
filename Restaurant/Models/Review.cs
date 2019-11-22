using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public User Customer { get; set; }

        public string Text { get; set; }
        public int Rating { get; set; }

        public int? DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
