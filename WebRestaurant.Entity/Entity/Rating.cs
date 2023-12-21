using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Domain.Entity;

namespace WebRestaurant.Entity.Entity
{
	public class Rating
	{
		public int Id { get; set; }
		public int Rate { get; set; }
		public int UserId { get; set; }
		public int DishId { get; set; }
		public User User { get; set; }
		public Dish Dish { get; set; }
	}
}
