using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Domain.Entity
{
    public class DishesToOrder
    {
        public int Id { get; set; }
        [DisplayName("Блюдо")]
        public int DishId { get; set; }
        [DisplayName("Заказ")]
        public int OrderId { get; set; }
        [DisplayName("Количество")]
        public int Amount { get; set; }
        [DisplayName("Блюдо")]
        public Dish Dish { get; set; }
        [DisplayName("Заказ")]
        public Order Order { get; set; }
    }
}
