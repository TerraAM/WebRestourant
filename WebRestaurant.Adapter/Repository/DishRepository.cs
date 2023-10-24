using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class DishRepository : IRepository<Dish>
    {
        private WebDbContext context;

        public DishRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Dish entity)
        { 
            await context.Dishes.AddAsync(entity);
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            var ent = await context.Dishes
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return context.Dishes;
        }

        public async Task UpdateAsync(Dish entity)
        {
            context.Dishes.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.Dishes
                .FirstOrDefaultAsync(x => x.Id == id);
            context.Dishes.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
