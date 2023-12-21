using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Shared.Dtos
{
	public class RatingDto
	{
		public int Id { get; set; }
		public int Rate { get; set; }
		public int UserId { get; set; }
		public int DishId { get; set; }
		public UserDto User { get; set; }
		public DishDto Dish { get; set; }
	}
}
