using eRestaurant.API.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace eRestaurant.API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDishRepository Dishes { get; }
        IRepository<DishType> DishTypes { get; }
        IOrderRepository Orders { get; }
        IRepository<OrderStatus> OrderStatuses { get; }
        IRepository<Review> Reviews { get; }
        IRepository<UnitOfMeasurement> Units { get; }
        IRepository<Waiter> Waiters { get; }
        IRepository<Customer> Customers { get; }
        IRepository<UserProfile> Profiles { get; }
        IRepository<DishImage> Images { get; }
        ICartRepository Carts { get; }

        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }

        void ModifyState(object entity);
        Task SaveChangesAsync();
    }
}
