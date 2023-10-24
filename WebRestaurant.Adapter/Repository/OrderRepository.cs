using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private WebDbContext context;

        public OrderRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Order entity)
        { 
            await context.Orders.AddAsync(entity);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var ent = await context.Orders
                .Include(d=>d.DinnerTable)
                .Include(c=>c.Client)
                .Include(s=>s.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return context.Orders
                .Include(d => d.DinnerTable)
                .Include(c => c.Client)
                .Include(s => s.Status);
        }

        public async Task UpdateAsync(Order entity)
        {
            context.Orders.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.Orders
                .Include(d => d.DinnerTable)
                .Include(c => c.Client)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
            context.Orders.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
