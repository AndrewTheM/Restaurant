using eRestaurant.Entities;
using System.Collections.Generic;

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
