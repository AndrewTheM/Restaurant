using eRestaurant.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.API.Repositories
{
    public interface ICartRepository : IRepository<OrderCart>
    {
        IEnumerable<int> GetDishesIds(int id);
    }
}
