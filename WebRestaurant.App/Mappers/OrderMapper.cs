using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order Order)
        {
            if (Order == null)
            {
                return null;
            }

            OrderDto OrderDto = new OrderDto()
            {
                Id = Order.Id,
                DinnerTableId = Order.DinnerTableId,
                ClientId = Order.ClientId,
                DateCreate = Order.DateCreate,
                Duration = Order.Duration,
                Price = Order.Price,
                StatusId = Order.StatusId,
                DinnerTable = Order.DinnerTable.ToDto(),
                Client = Order.Client.ToDto(),
                Status = Order.Status.ToDto()
            };

            return OrderDto;
        }
        public static Order ToEntity(this OrderDto OrderDto)
        {
            if (OrderDto == null)
            {
                return null;
            }

            Order Order = new Order()
            {
                Id = OrderDto.Id,
                DinnerTableId = OrderDto.DinnerTableId,
                ClientId = OrderDto.ClientId,
                DateCreate = OrderDto.DateCreate,
                Duration = OrderDto.Duration,
                Price = OrderDto.Price,
                StatusId = OrderDto.StatusId,
                DinnerTable = OrderDto.DinnerTable.ToEntity(),
                Client = OrderDto.Client.ToEntity(),
                Status = OrderDto.Status.ToEntity()
            };

            return Order;
        }
    }
}
