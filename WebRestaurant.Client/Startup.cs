using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Repository;
using WebRestaurant.Adapter.Services;
using WebRestaurant.Adapter.Transaction;
using WebRestaurant.App.Data;
using WebRestaurant.App.Interactors;
using WebRestaurant.Client.Services;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Entity.Entity;

namespace WebRestaurant.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "MySessionCookie";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddDbContext<WebDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("MyConnection"))
            );

            services.AddTransient<IUnitWork, UnitWork>();

            services.AddScoped<DinnerTableInteractor>();
            services.AddScoped<DishesToOrderInteractor>();
            services.AddScoped<DishInteractor>();
            services.AddScoped<OrderInteractor>();
            services.AddScoped<OrderStatusInteractor>();
            services.AddScoped<RoleInteractor>();
            services.AddScoped<UserInteractor>();
			services.AddScoped<CommentInteractor>();
			services.AddScoped<RatingInteractor>();
			services.AddScoped<FeedBackInteractor>();

			services.AddScoped<IRepository<DinnerTable>, DinnerTableRepository>();
            services.AddScoped<IRepository<DishesToOrder>, DishesToOrderRepository>();
            services.AddScoped<IRepository<Dish>, DishRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderStatus>, OrderStatusRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
			services.AddScoped<IRepository<Comment>, CommentRepository>();
			services.AddScoped<IRepository<Rating>, RatingRepository>();
			services.AddScoped<IRepository<FeedBack>, FeedBackRepository>();
		}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
