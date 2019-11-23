using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Entities
{
    public class Waiter
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
