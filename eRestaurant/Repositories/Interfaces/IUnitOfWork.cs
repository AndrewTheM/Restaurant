using eRestaurant.Entities;
using Microsoft.AspNetCore.Identity;
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
        IRepository<Waiter> Waiters { get; }
        IRepository<Customer> Customers { get; }
        IRepository<UserProfile> Profiles { get; }

        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }


        void SaveChanges();
    }
}
