using Microsoft.EntityFrameworkCore;
using WebRestaurant.Domain;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Entity.Entity;

namespace WebRestaurant.Adapter.Services
{
    public class WebDbContext : DbContext
    {
        public DbSet<DinnerTable> DinnerTables { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishesToOrder> DishesToOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }
        public WebDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			string adminRoleName = "admin";
			string userRoleName = "user";
			string waiterRoleName = "waiter";

			string adminEmail = "admin@mail.ru";
			string adminPassword = "321";

			string userEmail = "1@1.com";
			string userPassword = "123";

			string waitingOrderStatus = "На ожидании";
			string inProgressOrderStatus = "В процессе";
			string compliteOrderStatus = "Готов";

			Role adminRole = new Role { Id = 1, Name = adminRoleName };
			Role userRole = new Role { Id = 2, Name = userRoleName };
			Role waiterRole = new Role { Id = 3, Name = waiterRoleName };
			User Anonymous = new User { Id = 1,Name = "anon", Email = "anon@mail.com", Password = "anon", RoleId = 2 };
			User adminUser = new User { Id = 2, Name = "admin", Email = adminEmail, Password = adminPassword, RoleId = 1 };
			User User = new User { Id = 3, Name = "user", Email = userEmail, Password = userPassword, RoleId = 2 };
			User waiter = new User { Id = 4, Name = "waiter", Email = "waiter@mail.com", Password = "1", RoleId = 3 };
			OrderStatus status1 = new OrderStatus() { Id = 1, Name = waitingOrderStatus };
			OrderStatus status2 = new OrderStatus() { Id = 2, Name = inProgressOrderStatus };
			OrderStatus status3 = new OrderStatus() { Id = 3, Name = compliteOrderStatus };

			modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, waiterRole });
			modelBuilder.Entity<User>().HasData(new User[] { Anonymous, adminUser, User, waiter });
			modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus[] { status1, status2, status3 });
			modelBuilder.Entity<DinnerTable>().HasData(new DinnerTable() {Id = 1 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
