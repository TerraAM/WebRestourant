using System;
using System.ComponentModel;

namespace WebRestaurant.Shared.Dtos
{
    public class OrderDto
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
        public DinnerTableDto DinnerTable { get; set; }
        [DisplayName("Клиент")]
        public UserDto Client { get; set; }
        [DisplayName("Статус")]
        public OrderStatusDto Status { get; set; }
    }
}
