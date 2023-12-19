using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Shared.Model
{
	public class DishModel
	{
		public DishDto Dish { get; set; } = new DishDto();
		public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
	}
}
