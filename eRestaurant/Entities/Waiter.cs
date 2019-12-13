using System.Collections.Generic;

namespace eRestaurant.Entities
{
    public class Waiter
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
