using System.Collections.Generic;

namespace eRestaurant.API.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }

        public IList<Order> Orders { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}
