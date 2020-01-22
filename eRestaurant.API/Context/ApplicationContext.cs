using eRestaurant.API.Entities;
using eRestaurant.API.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace eRestaurant.API.Context
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
        public DbSet<OrderCart> Carts { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DishType>(e =>
            {
                e.HasKey(dt => dt.Id);
                e.HasIndex(dt => dt.Id);
                e.HasData(
                    new DishType { Id = 1, Name = "Salad" },
                    new DishType { Id = 2, Name = "Pizza" },
                    new DishType { Id = 3, Name = "Drink" }
                    );
            });

            modelBuilder.Entity<OrderStatus>(e =>
            {
                e.HasKey(os => os.Id);
                e.HasIndex(os => os.Id);
                e.HasData(
                    new OrderStatus { Id = 1, Name = "Pending" },
                    new OrderStatus { Id = 2, Name = "Active" }
                    );
            });

            modelBuilder.Entity<UnitOfMeasurement>(e =>
            {
                e.HasKey(uom => uom.Id);
                e.HasIndex(uom => uom.Id);
                e.HasData(
                    new UnitOfMeasurement { Id = 1, Name = "g" },
                    new UnitOfMeasurement { Id = 2, Name = "ml" }
                    );
            });

            modelBuilder.Entity<Dish>(e =>
            {
                e.HasKey(d => d.Id);
                e.HasIndex(d => d.Id);
                e.HasOne(d => d.Type).WithMany(dt => dt.Dishes).HasForeignKey(d => d.TypeId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(d => d.PortionUnit).WithMany(uom => uom.Dishes).HasForeignKey(d => d.UnitId).OnDelete(DeleteBehavior.SetNull);
                e.HasData(
                    new Dish { Id = 1, Name = "Caesar", Description = "Very tasty salad", Price = 10, PortionSize = 250, CookingTime = TimeSpan.FromMinutes(10), TypeId = 1, UnitId = 1 },
                    new Dish { Id = 2, Name = "Margarita", Description = "Nice pizza", Price = 15, PortionSize = 300, CookingTime = TimeSpan.FromMinutes(17.5), TypeId = 2, UnitId = 1 },
                    new Dish { Id = 3, Name = "Coca Cola", Description = "Cool drink", Price = 5, PortionSize = 500, TypeId = 3, UnitId = 2 }
                    );
            });

            modelBuilder.Entity<DishImage>(e =>
            {
                e.HasKey(di => di.Id);
                e.HasIndex(di => di.Id);
                e.HasOne(di => di.Dish).WithMany(d => d.Images).HasForeignKey(di => di.DishId).OnDelete(DeleteBehavior.Cascade);
                e.HasData(
                    new DishImage { Id = 1, DishId = 1, Image = Image.FromFile("Media/Caesar.jpg").ToByteArray() },
                    new DishImage { Id = 2, DishId = 2, Image = Image.FromFile("Media/Margarita.jpg").ToByteArray() },
                    new DishImage { Id = 3, DishId = 3, Image = Image.FromFile("Media/CocaCola.jpg").ToByteArray() }
                    );
            });

            modelBuilder.Entity<OrderCart>(e =>
            {
                e.HasKey(oc => oc.Id);
                e.HasIndex(oc => oc.Id);
                e.HasData(
                    new OrderCart { Id = 1 },
                    new OrderCart { Id = 2 },
                    new OrderCart { Id = 3 }
                    );
            });

            modelBuilder.Entity<CartDish>(e =>
            {
                e.HasKey(cd => new { cd.CartId, cd.DishId });
                e.HasIndex(cd => new { cd.CartId, cd.DishId });
                e.HasOne(cd => cd.Cart).WithMany(oc => oc.CartDishes).HasForeignKey(cd => cd.CartId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(cd => cd.Dish).WithMany(d => d.CartDishes).HasForeignKey(cd => cd.DishId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserProfile>(e =>
            {
                e.HasKey(up => up.Id);
                e.HasIndex(up => up.Id);
                e.HasOne(up => up.User).WithOne(u => u.Profile).HasForeignKey<UserProfile>(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(up => up.Cart).WithOne(oc => oc.Profile).HasForeignKey<UserProfile>(up => up.CartId).OnDelete(DeleteBehavior.Cascade);
                e.HasData(
                    new UserProfile { Id = 1, DateOfBirth = DateTime.Today.AddYears(-30), CartId = 1 },
                    new UserProfile { Id = 2, DateOfBirth = DateTime.Today.AddYears(-46), CartId = 2 },
                    new UserProfile { Id = 3, CartId = 3 }
                    );
            });


            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(c => c.Id);
                e.HasIndex(c => c.Id);
                e.HasOne(c => c.Profile).WithOne(up => up.Customer).HasForeignKey<Customer>(c => c.ProfileId).OnDelete(DeleteBehavior.Cascade);
                e.HasData(
                    new Customer { Id = 1, ProfileId = 1 },
                    new Customer { Id = 2, ProfileId = 2 }
                    );
            });

            modelBuilder.Entity<Waiter>(e =>
            {
                e.HasKey(w => w.Id);
                e.HasIndex(w => w.Id);
                e.HasOne(w => w.Profile).WithOne(up => up.Waiter).HasForeignKey<Waiter>(w => w.ProfileId).OnDelete(DeleteBehavior.Cascade);
                e.HasData(
                    new Waiter { Id = 1, ProfileId = 3 }
                    );
            });

            modelBuilder.Entity<Review>(e =>
            {
                e.HasKey(r => r.Id);
                e.HasIndex(r => r.Id);
                e.HasOne(r => r.Dish).WithMany(d => d.Reviews).HasForeignKey(r => r.DishId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(r => r.Customer).WithMany(c => c.Reviews).HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.SetNull);
                e.HasData(
                    new Review { Id = 1, CustomerId = 2, Text = "So tasty!", Rating = 5, DishId = 2 },
                    new Review { Id = 2, CustomerId = 1, Text = "Too much sugar!", Rating = 3, DishId = 3 }
                    );
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.Id);
                e.HasIndex(o => o.Id);
                e.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(o => o.Status).WithMany(os => os.Orders).HasForeignKey(o => o.StatusId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(o => o.Waiter).WithMany(w => w.Orders).HasForeignKey(o => o.WaiterId);
                e.HasData(
                    new Order { Id = 1, CustomerId = 2, StatusId = 1, WaiterId = 1, TableNbr = 23, Tips = true, Date = DateTime.Today },
                    new Order { Id = 2, CustomerId = 1, StatusId = 2, WaiterId = 1, TableNbr = 16, Tips = false, EstimatedTime = TimeSpan.FromMinutes(20) }
                    );
            });

            modelBuilder.Entity<OrderDish>(e =>
            {
                e.HasKey(od => new { od.OrderId, od.DishId });
                e.HasIndex(od => new { od.OrderId, od.DishId });
                e.HasOne(od => od.Order).WithMany(o => o.OrderDishes).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(od => od.Dish).WithMany(d => d.OrderDishes).HasForeignKey(od => od.DishId).OnDelete(DeleteBehavior.Cascade);
                e.HasData(
                    new OrderDish { OrderId = 1, DishId = 3, Quantity = 2 },
                    new OrderDish { OrderId = 2, DishId = 1, Quantity = 1 },
                    new OrderDish { OrderId = 2, DishId = 2, Quantity = 1 }
                    );
            });
        }
    }
}
