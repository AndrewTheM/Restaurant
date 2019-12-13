using System.Collections.Generic;

namespace eRestaurant.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
