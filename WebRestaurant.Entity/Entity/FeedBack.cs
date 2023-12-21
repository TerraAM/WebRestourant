using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Entity.Entity
{
	public class FeedBack
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }
		public DateTime CreateTime { get; set; }
	}
}
