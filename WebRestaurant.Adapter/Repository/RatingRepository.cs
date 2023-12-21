using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Entity.Entity;

namespace WebRestaurant.Adapter.Repository
{
	public class RatingRepository : IRepository<Rating>
	{
		private WebDbContext context;

		public RatingRepository(WebDbContext context)
		{
			this.context = context;
		}

		public async Task CreateAsync(Rating entity)
		{
			await context.Ratings.AddAsync(entity);
		}

		public async Task<Rating> GetByIdAsync(int id)
		{
			var ent = await context.Ratings
				.Include(u => u.User)
				.Include(d => d.Dish)
				.FirstOrDefaultAsync(x => x.Id == id);
			return ent ?? throw new NullReferenceException();
		}

		public async Task<IEnumerable<Rating>> GetAllAsync()
		{
			return context.Ratings
				.Include(u => u.User);
		}

		public async Task UpdateAsync(Rating entity)
		{
			context.Ratings.Update(entity);
		}

		public async Task DeleteByIdAsync(int id)
		{
			var ent = await context.Ratings
				.Include(u => u.User)
				.Include(d => d.Dish)
				.FirstOrDefaultAsync(x => x.Id == id);
			context.Ratings.Remove(ent ?? throw new NullReferenceException());
		}
	}
}
