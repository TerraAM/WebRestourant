using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Data;
using WebRestaurant.Entity.Entity;

namespace WebRestaurant.Adapter.Repository
{
	public class CommentRepository : IRepository<Comment>
	{
		private WebDbContext context;

		public CommentRepository(WebDbContext context)
		{
			this.context = context;
		}

		public async Task CreateAsync(Comment entity)
		{
			await context.Comments.AddAsync(entity);
		}

		public async Task<Comment> GetByIdAsync(int id)
		{
			var ent = await context.Comments
				.Include(u=>u.User)
				.Include(d=>d.Dish)
				.FirstOrDefaultAsync(x => x.Id == id);
			return ent ?? throw new NullReferenceException();
		}

		public async Task<IEnumerable<Comment>> GetAllAsync()
		{
			return context.Comments
				.Include(u=>u.User);
		}

		public async Task UpdateAsync(Comment entity)
		{
			context.Comments.Update(entity);
		}

		public async Task DeleteByIdAsync(int id)
		{
			var ent = await context.Comments
				.Include(u => u.User)
				.Include(d => d.Dish)
				.FirstOrDefaultAsync(x => x.Id == id);
			context.Comments.Remove(ent ?? throw new NullReferenceException());
		}
	}
}
