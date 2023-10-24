using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Adapter.Repository
{
    public class UserRepository : IRepository<User>
    {
        private WebDbContext context;

        public UserRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(User entity)
        { 
            await context.Users.AddAsync(entity);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var ent = await context.Users
                .Include(r=>r.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ent ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return context.Users
                .Include(r=>r.Role);
        }

        public async Task UpdateAsync(User entity)
        {
            context.Users.Update(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ent = await context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            context.Users.Remove(ent ?? throw new NullReferenceException());
        }       
    }
}
