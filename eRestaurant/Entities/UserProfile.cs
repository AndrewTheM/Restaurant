using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Entities
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<Order> Orders { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? WaiterId { get; set; }
        public Waiter Waiter { get; set; }
    }
}
