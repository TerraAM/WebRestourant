using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class OrderStatusMapper
    {
        public static OrderStatusDto ToDto(this OrderStatus OrderStatus)
        {
            if (OrderStatus == null)
            {
                return null;
            }

            OrderStatusDto OrderStatusDto = new OrderStatusDto()
            {
                Id = OrderStatus.Id,
                Name = OrderStatus.Name
            };

            return OrderStatusDto;
        }
        public static OrderStatus ToEntity(this OrderStatusDto OrderStatusDto)
        {
            if (OrderStatusDto == null)
            {
                return null;
            }

            OrderStatus OrderStatus = new OrderStatus()
            {
                Id = OrderStatusDto.Id,
                Name = OrderStatusDto.Name
            };

            return OrderStatus;
        }
    }
}
