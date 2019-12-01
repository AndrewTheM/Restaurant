using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using eRestaurant;
using eRestaurant.Repositories;
using eRestaurant.Entities;
using Microsoft.AspNetCore.Identity;
using eRestaurant.Services;

namespace eRestaurant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServerConn")));
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddEntityFrameworkStores<ApplicationContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IRepository<DishType>, Repository<DishType>>();
            services.AddTransient<IRepository<OrderStatus>, Repository<OrderStatus>>();
            services.AddTransient<IRepository<Review>, Repository<Review>>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Customer>, Repository<Customer>>();
            services.AddTransient<IRepository<Waiter>, Repository<Waiter>>();
            services.AddTransient<IRepository<UnitOfMeasurement>, Repository<UnitOfMeasurement>>();
            services.AddTransient<IRepository<UserProfile>, Repository<UserProfile>>();
            services.AddTransient<IRepository<DishImage>, Repository<DishImage>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<UserManager<User>>();
            services.AddTransient<RoleManager<IdentityRole>>();
            services.AddTransient<SignInManager<User>>();

            services.AddTransient<IIdentityService, IdentityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
