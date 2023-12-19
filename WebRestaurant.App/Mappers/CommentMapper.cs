using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
	public static class CommentMapper
	{
		public static CommentDto ToDto(this Comment Comment)
		{
			if (Comment == null)
			{
				return null;
			}

			CommentDto CommentDto = new CommentDto()
			{
				Id = Comment.Id,
				Content = Comment.Content,
				UserId = Comment.UserId,
				DishId = Comment.DishId,
				CreatedDate = Comment.CreatedDate,
				User = Comment.User.ToDto(),
				Dish = Comment.Dish.ToDto(),
			};

			return CommentDto;
		}
		public static Comment ToEntity(this CommentDto CommentDto)
		{
			if (CommentDto == null)
			{
				return null;
			}

			Comment Comment = new Comment()
			{
				Id = CommentDto.Id,
				Content = CommentDto.Content,
				UserId = CommentDto.UserId,
				DishId = CommentDto.DishId,
				CreatedDate = CommentDto.CreatedDate,
				User = CommentDto.User.ToEntity(),
				Dish = CommentDto.Dish.ToEntity(),
			};

			return Comment;
		}
	}
}
