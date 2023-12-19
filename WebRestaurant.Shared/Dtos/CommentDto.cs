using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Shared.Dtos
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public int UserId { get; set; }
		public int DishId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public UserDto User { get; set; }
		public DishDto Dish { get; set; }
	}
}
