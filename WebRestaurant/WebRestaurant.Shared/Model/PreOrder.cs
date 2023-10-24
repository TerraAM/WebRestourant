using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Shared.Model
{
    public class PreOrder
    {
        [DisplayName("Блюдо")]
        public int DishId { get; set; }
        [DisplayName("Количество")]
        public int Amount { get; set; }
        public DishDto Dish { get; set; } = new DishDto();
    }
}
