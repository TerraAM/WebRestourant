using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class OrderStatusRepository : IRepository<OrderStatus>
    {
        private WebDbContext context;

        public OrderStatusRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(OrderStatus entity)
        { 
            await context.OrderStatuses.AddAsync(entity);
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            var ent = await context.OrderStatuses
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return context.OrderStatuses;
        }

        public async Task UpdateAsync(OrderStatus entity)
        {
            context.OrderStatuses.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.OrderStatuses
                .FirstOrDefaultAsync(x => x.Id == id);
            context.OrderStatuses.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
