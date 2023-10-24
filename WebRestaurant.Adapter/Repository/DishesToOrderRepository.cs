using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class DishesToOrderRepository : IRepository<DishesToOrder>
    {
        private WebDbContext context;

        public DishesToOrderRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(DishesToOrder entity)
        { 
            await context.DishesToOrders.AddAsync(entity);
        }

        public async Task<DishesToOrder> GetByIdAsync(int id)
        {
            var ent = await context.DishesToOrders
                .Include(d=>d.Dish)
                .Include(o=>o.Order)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<DishesToOrder>> GetAllAsync()
        {
            return context.DishesToOrders
                 .Include(d => d.Dish)
                 .Include(o => o.Order);
        }

        public async Task UpdateAsync(DishesToOrder entity)
        {
            context.DishesToOrders.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.DishesToOrders
                .Include(d => d.Dish)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(x => x.Id == id);
            context.DishesToOrders.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
