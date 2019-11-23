﻿using Microsoft.EntityFrameworkCore;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDish>().HasKey(od => new { od.OrderId, od.DishId });
            modelBuilder.Entity<OrderDish>().HasOne(od => od.Order).WithMany(o => o.OrderDishes).HasForeignKey(od => od.OrderId);
            modelBuilder.Entity<OrderDish>().HasOne(od => od.Dish).WithMany(d => d.OrderDishes).HasForeignKey(od => od.DishId);
        }
    }
}
