using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public int? ProfileId { get; set; }
        public UserProfile Profile { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
