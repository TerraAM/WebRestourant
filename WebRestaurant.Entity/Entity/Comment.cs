using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Entity.Entity
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public int UserId { get; set; }
		public int DishId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public User User { get; set; }
		public Dish Dish { get; set; }
	}
}
