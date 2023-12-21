using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
	public static class FeedBackMapper
	{
		public static FeedBackDto ToDto(this FeedBack FeedBack)
		{
			if (FeedBack == null)
			{
				return null;
			}

			FeedBackDto FeedBackDto = new FeedBackDto()
			{
				Id = FeedBack.Id,
				Email = FeedBack.Email,
				Title = FeedBack.Title,
				Context = FeedBack.Context,
				CreateTime = FeedBack.CreateTime
			};

			return FeedBackDto;
		}
		public static FeedBack ToEntity(this FeedBackDto FeedBackDto)
		{
			if (FeedBackDto == null)
			{
				return null;
			}

			FeedBack FeedBack = new FeedBack()
			{
				Id = FeedBackDto.Id,
				Email = FeedBackDto.Email,
				Title = FeedBackDto.Title,
				Context = FeedBackDto.Context,
				CreateTime = FeedBackDto.CreateTime
			};

			return FeedBack;
		}
	}
}
