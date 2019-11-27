using Microsoft.EntityFrameworkCore;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eRestaurant
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDish>().HasKey(od => new { od.OrderId, od.DishId });
            modelBuilder.Entity<OrderDish>().HasOne(od => od.Order).WithMany(o => o.OrderDishes).HasForeignKey(od => od.OrderId);
            modelBuilder.Entity<OrderDish>().HasOne(od => od.Dish).WithMany(d => d.OrderDishes).HasForeignKey(od => od.DishId);

            modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<UserProfile>(p => p.UserId);
        }
    }
}
