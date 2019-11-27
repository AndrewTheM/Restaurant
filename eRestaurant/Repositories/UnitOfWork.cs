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
                IRepository<Review> reviews, IRepository<Unit> units, 
                IRepository<User> users, IRepository<Waiter> waiters,
                IRepository<Customer> customers, UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            Dishes = dishes;
            DishTypes = dishTypes;
            Orders = orders;
            OrderStatuses = orderStatuses;
            Reviews = reviews;
            Units = units;
            Users = users;
            Waiters = waiters;
            Customers = customers;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public SignInManager<User> SignInManager { get; private set; }
        
        public IDishRepository Dishes { get; }
        public IRepository<DishType> DishTypes { get; }
        public IOrderRepository Orders { get; }
        public IRepository<OrderStatus> OrderStatuses { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Unit> Units { get; }
        public IRepository<User> Users { get; }
        public IRepository<Waiter> Waiters { get; }
        public IRepository<Customer> Customers { get; }

        public void Dispose() => _context.Dispose();

        public void SaveChanges() => _context.SaveChanges();
    }
}
