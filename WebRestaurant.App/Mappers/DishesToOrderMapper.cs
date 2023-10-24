using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class DishesToOrderMapper
    {
        public static DishesToOrderDto ToDto(this DishesToOrder DishesToOrder)
        {
            if (DishesToOrder == null)
            {
                return null;
            }

            DishesToOrderDto DishesToOrderDto = new DishesToOrderDto()
            {
                Id = DishesToOrder.Id,
                DishId = DishesToOrder.DishId,
                OrderId = DishesToOrder.OrderId,
                Amount = DishesToOrder.Amount,
                Dish = DishesToOrder.Dish.ToDto(),
                Order = DishesToOrder.Order.ToDto()
            };

            return DishesToOrderDto;
        }
        public static DishesToOrder ToEntity(this DishesToOrderDto DishesToOrderDto)
        {
            if (DishesToOrderDto == null)
            {
                return null;
            }

            DishesToOrder DishesToOrder = new DishesToOrder()
            {
                Id = DishesToOrderDto.Id,
                DishId = DishesToOrderDto.DishId,
                OrderId = DishesToOrderDto.OrderId,
                Amount = DishesToOrderDto.Amount,
                Dish = DishesToOrderDto.Dish.ToEntity(),
                Order = DishesToOrderDto.Order.ToEntity()
            };

            return DishesToOrder;
        }
    }
}
