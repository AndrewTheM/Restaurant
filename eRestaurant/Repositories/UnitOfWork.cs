using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestaurant.Entities;
using Microsoft.AspNetCore.Identity;

namespace eRestaurant.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context,
                IDishRepository dishes, IRepository<DishType> dishTypes,
                IOrderRepository orders, IRepository<OrderStatus> orderStatuses,
                IRepository<Review> reviews, IRepository<UnitOfMeasurement> units,
                IRepository<Waiter> waiters, IRepository<Customer> customers,
                IRepository<UserProfile> profiles, IRepository<DishImage> images,
                UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                SignInManager<User> signInManager)
        {
            _context = context;
            Dishes = dishes;
            DishTypes = dishTypes;
            Orders = orders;
            OrderStatuses = orderStatuses;
            Reviews = reviews;
            Units = units;
            Waiters = waiters;
            Customers = customers;
            Profiles = profiles;
            Images = images;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<User> SignInManager { get; }
        
        public IDishRepository Dishes { get; }
        public IRepository<DishType> DishTypes { get; }
        public IOrderRepository Orders { get; }
        public IRepository<OrderStatus> OrderStatuses { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<UnitOfMeasurement> Units { get; }
        public IRepository<Waiter> Waiters { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<UserProfile> Profiles { get; }
        public IRepository<DishImage> Images { get; }

        public void Dispose() => _context.Dispose();

        public void SaveChanges() => _context.SaveChanges();
    }
}
