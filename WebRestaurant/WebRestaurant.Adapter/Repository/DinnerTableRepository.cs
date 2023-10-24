using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class DinnerTableRepository : IRepository<DinnerTable>
    {
        private WebDbContext context;

        public DinnerTableRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(DinnerTable entity)
        { 
            await context.DinnerTables.AddAsync(entity);
        }

        public async Task<DinnerTable> GetByIdAsync(int id)
        {
            var ent = await context.DinnerTables
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<DinnerTable>> GetAllAsync()
        {
            return context.DinnerTables;
        }

        public async Task UpdateAsync(DinnerTable entity)
        {
            context.DinnerTables.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.DinnerTables
                .FirstOrDefaultAsync(x => x.Id == id);
            context.DinnerTables.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
