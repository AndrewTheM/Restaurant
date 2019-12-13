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
        public DbSet<UnitOfMeasurement> Units { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<DishImage> Images { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(c => c.Id);
                e.HasIndex(c => c.Id);
                e.HasOne(c => c.Profile).WithOne(up => up.Customer).HasForeignKey<UserProfile>(p => p.CustomerId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Dish>(e =>
            {
                e.HasKey(d => d.Id);
                e.HasIndex(d => d.Id);
                e.HasOne(d => d.Type).WithMany(dt => dt.Dishes).HasForeignKey(d => d.TypeId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(d => d.PortionUnit).WithMany(uom => uom.Dishes).HasForeignKey(d => d.UnitId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<DishImage>(e =>
            {
                e.HasKey(di => di.Id);
                e.HasIndex(di => di.Id);
                e.HasOne(di => di.Dish).WithMany(d => d.Images).HasForeignKey(di => di.DishId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DishType>(e =>
            {
                e.HasKey(dt => dt.Id);
                e.HasIndex(dt => dt.Id);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.Id);
                e.HasIndex(o => o.Id);
                e.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(o => o.Waiter).WithMany(w => w.Orders).HasForeignKey(o => o.WaiterId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(o => o.Status).WithMany(os => os.Orders).HasForeignKey(o => o.WaiterId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<OrderDish>(e =>
            {
                e.HasKey(od => new { od.OrderId, od.DishId });
                e.HasIndex(od => new { od.OrderId, od.DishId });
                e.HasOne(od => od.Order).WithMany(o => o.OrderDishes).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(od => od.Dish).WithMany(d => d.OrderDishes).HasForeignKey(od => od.DishId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderStatus>(e =>
            {
                e.HasKey(os => os.Id);
                e.HasIndex(os => os.Id);
            });

            modelBuilder.Entity<Review>(e =>
            {
                e.HasKey(r => r.Id);
                e.HasIndex(r => r.Id);
                e.HasOne(r => r.Dish).WithMany(d => d.Reviews).HasForeignKey(r => r.DishId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(r => r.Customer).WithMany(c => c.Reviews).HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UnitOfMeasurement>(e =>
            {
                e.HasKey(uom => uom.Id);
                e.HasIndex(uom => uom.Id);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.Id);
                e.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<UserProfile>(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserProfile>(e =>
            {
                e.HasKey(up => up.Id);
                e.HasIndex(up => up.Id);
            });

            modelBuilder.Entity<Waiter>(e =>
            {
                e.HasKey(w => w.Id);
                e.HasIndex(w => w.Id);
                e.HasOne(w => w.Profile).WithOne(u => u.Waiter).HasForeignKey<UserProfile>(up => up.WaiterId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
