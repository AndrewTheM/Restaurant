using eRestaurant.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context) { }

        public double CalcTotalIncome() => GetAll().Sum(o => CalcTotalPrice(o.Id));

        public double CalcTotalPrice(int id) => GetDishesOfOrder(id).Sum(d => d.Price * d.Quantity);

        public IEnumerable<Order> GetByTableNbr(int tableNbr) => GetAll().Where(o => o.TableNbr == tableNbr);

        public IEnumerable<Dish> GetDishesOfOrder(int id) => Get(id).OrderDishes.Select(od => od.Dish);
    }
}
