using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Entity.Entity;

namespace WebRestaurant.Adapter.Repository
{
	public class FeedBackRepository : IRepository<FeedBack>
	{
		private WebDbContext context;

		public FeedBackRepository(WebDbContext context)
		{
			this.context = context;
		}

		public async Task CreateAsync(FeedBack entity)
		{
			await context.FeedBacks.AddAsync(entity);
		}

		public async Task<FeedBack> GetByIdAsync(int id)
		{
			var ent = await context.FeedBacks
				.FirstOrDefaultAsync(x => x.Id == id);
			return ent ?? throw new NullReferenceException();
		}

		public async Task<IEnumerable<FeedBack>> GetAllAsync()
		{
			return context.FeedBacks;
		}

		public async Task UpdateAsync(FeedBack entity)
		{
			context.FeedBacks.Update(entity);
		}

		public async Task DeleteByIdAsync(int id)
		{
			var ent = await context.FeedBacks
				.FirstOrDefaultAsync(x => x.Id == id);
			context.FeedBacks.Remove(ent ?? throw new NullReferenceException());
		}
	}
}
