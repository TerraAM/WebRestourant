using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.App.Data
{
    public interface IRepository<T> where T : class
    {
        public Task CreateAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task UpdateAsync(T entity);
        public Task DeleteByIdAsync(int id); 
    }
}
