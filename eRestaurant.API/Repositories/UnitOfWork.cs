using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestaurant.API.Context;
using eRestaurant.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.API.Repositories
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
                ICartRepository carts, UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
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
            Carts = carts;
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
        public ICartRepository Carts { get; }

        public void Dispose() => _context.Dispose();

        public void ModifyState(object entity) => _context.Entry(entity).State = EntityState.Modified;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
