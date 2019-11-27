using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetByTableNbr(int tableNbr);
        IEnumerable<Dish> GetDishesOfOrder(int id);
        double CalcTotalPrice(int id);
        double CalcTotalIncome();
    }
}
