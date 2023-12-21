using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.App.Data;
using WebRestaurant.App.Mappers;
using WebRestaurant.Domain.Data;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Interactors
{
	public class FeedBackInteractor
	{
		private IRepository<FeedBack> repos;
		private IUnitWork unitWork;

		public FeedBackInteractor(IRepository<FeedBack> repos, IUnitWork unitWork)
		{
			this.repos = repos;
			this.unitWork = unitWork;
		}
		public async Task<Response> Create(FeedBackDto FeedBackDto)
		{
			var response = new Response<FeedBackDto>();
			try
			{
				await repos.CreateAsync(FeedBackDto.ToEntity());
				await unitWork.Commit();
				return new Response() { IsSuccess = true };
			}
			catch (Exception ex)
			{
				return new Response()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка создания"
				};
			}
		}
		public async Task<Response> Delete(int id)
		{
			try
			{
				await repos.DeleteByIdAsync(id);
				await unitWork.Commit();
				return new Response()
				{
					IsSuccess = true
				};
			}
			catch (NullReferenceException ex)
			{
				return new Response()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Запись не найдена"
				};
			}
			catch (Exception ex)
			{
				return new Response()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка удаления"
				};
			}
		}
		public async Task<Response<FeedBackDto>> GetById(int id)
		{
			try
			{
				var entity = await repos.GetByIdAsync(id);
				return new Response<FeedBackDto>()
				{
					IsSuccess = true,
					Value = entity.ToDto()
				};
			}
			catch (NullReferenceException ex)
			{
				return new Response<FeedBackDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Запись не найдена"
				};
			}
			catch (Exception ex)
			{
				return new Response<FeedBackDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response<IEnumerable<FeedBackDto>>> GetAll()
		{
			try
			{
				var list = await repos.GetAllAsync();
				if (list == null)
					return new Response<IEnumerable<FeedBackDto>>()
					{
						IsSuccess = true,
						Value = null
					};
				else
					return new Response<IEnumerable<FeedBackDto>>()
					{
						IsSuccess = true,
						Value = list.Select(e => e.ToDto())
					};
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<FeedBackDto>>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response> Update(FeedBackDto CommentDto)
		{
			try
			{
				await repos.UpdateAsync(CommentDto.ToEntity());
				await unitWork.Commit();
				return new Response()
				{
					IsSuccess = true
				};
			}
			catch (NullReferenceException ex)
			{
				return new Response()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Запись не найдена"
				};
			}
			catch (Exception ex)
			{
				return new Response()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}
	}
}
