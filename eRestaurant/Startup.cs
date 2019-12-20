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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using AutoMapper;

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
            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServerConn")));
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

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

            var key = Encoding.ASCII.GetBytes("Secret key for JWT tokens");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eRestaurant", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                                  Enter 'Bearer' [space] and then your token in the text input below.
                                  Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });
            services.AddAutoMapper(typeof(Startup));


            services.AddControllers();

            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRepository<DishType>, Repository<DishType>>();
            services.AddScoped<IRepository<OrderStatus>, Repository<OrderStatus>>();
            services.AddScoped<IRepository<Review>, Repository<Review>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Waiter>, Repository<Waiter>>();
            services.AddScoped<IRepository<UnitOfMeasurement>, Repository<UnitOfMeasurement>>();
            services.AddScoped<IRepository<UserProfile>, Repository<UserProfile>>();
            services.AddScoped<IRepository<DishImage>, Repository<DishImage>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<SignInManager<User>>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMenuService, MenuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Blazor.Startup>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eRestaurant V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToClientSideBlazor<Blazor.Startup>("index.html");
            });
        }
    }
}
