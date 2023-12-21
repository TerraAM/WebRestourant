using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
	public static class RatingMapper
	{
		public static RatingDto ToDto(this Rating Rating)
		{
			if (Rating == null)
			{
				return null;
			}

			RatingDto RatingDto = new RatingDto()
			{
				Id = Rating.Id,
				Rate = Rating.Rate,
				UserId = Rating.UserId,
				DishId = Rating.DishId,
				User = Rating.User.ToDto(),
				Dish = Rating.Dish.ToDto()
			};

			return RatingDto;
		}
		public static Rating ToEntity(this RatingDto RatingDto)
		{
			if (RatingDto == null)
			{
				return null;
			}

			Rating Rating = new Rating()
			{
				Id = RatingDto.Id,
				Rate = RatingDto.Rate,
				UserId = RatingDto.UserId,
				DishId = RatingDto.DishId,
				User = RatingDto.User.ToEntity(),
				Dish = RatingDto.Dish.ToEntity()
			};

			return Rating;
		}
	}
}
