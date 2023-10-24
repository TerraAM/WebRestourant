using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class DishMapper
    {
        public static DishDto ToDto(this Dish Dish)
        {
            if (Dish == null)
            {
                return null;
            }

            DishDto DishDto = new DishDto()
            {
                Id = Dish.Id,
                Name = Dish.Name,
                Price = Dish.Price,
                Weight = Dish.Weight,
                PhotoPath = Dish.PhotoPath,
                Description = Dish.Description
            };

            return DishDto;
        }
        public static Dish ToEntity(this DishDto DishDto)
        {
            if (DishDto == null)
            {
                return null;
            }

            Dish Dish = new Dish()
            {
                Id = DishDto.Id,
                Name = DishDto.Name,
                Price = DishDto.Price,
                Weight = DishDto.Weight,
                PhotoPath = DishDto.PhotoPath,
                Description = DishDto.Description
            };

            return Dish;
        }
    }
}
