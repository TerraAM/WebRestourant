using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        [DisplayName("Столик")]
        public int DinnerTableId { get; set; }
        [DisplayName("Клиент")]
        public int ClientId { get; set; }
        [DisplayName("Время заказа")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Продолжительность")]
        public int Duration { get; set; }
        [DisplayName("Цена")]
        public double Price { get; set; }
        [DisplayName("Статус")]
        public int StatusId { get; set; }
        [DisplayName("Столик")]
        public DinnerTable DinnerTable { get; set; }
        [DisplayName("Клиент")]
        public User Client { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
    }
}
