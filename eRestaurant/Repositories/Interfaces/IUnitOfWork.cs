using eRestaurant.Entities;
using System;

namespace eRestaurant.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDishRepository Dishes { get; }
        IRepository<DishType> DishTypes { get; }
        IOrderRepository Orders { get; }
        IRepository<OrderStatus> OrderStatuses { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Unit> Units { get; }
        IRepository<User> Users { get; }
        IRepository<Waiter> Waiters { get; }
        IRepository<Customer> Customers { get; }

        void SaveChanges();
    }
}
