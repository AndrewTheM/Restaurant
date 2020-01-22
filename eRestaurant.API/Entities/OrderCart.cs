using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.API.Entities
{
    public class OrderCart
    {
        public int Id { get; set; }

        public UserProfile Profile { get; set; }

        public IList<CartDish> CartDishes { get; set; }
    }
}
