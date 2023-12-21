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
	public class RatingInteractor
	{
		private IRepository<Rating> repos;
		private IUnitWork unitWork;

		public RatingInteractor(IRepository<Rating> repos, IUnitWork unitWork)
		{
			this.repos = repos;
			this.unitWork = unitWork;
		}
		public async Task<Response> Create(RatingDto RatingDto)
		{
			var response = new Response<RatingDto>();
			try
			{
				await repos.CreateAsync(RatingDto.ToEntity());
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
		public async Task<Response<RatingDto>> GetById(int id)
		{
			try
			{
				var entity = await repos.GetByIdAsync(id);
				return new Response<RatingDto>()
				{
					IsSuccess = true,
					Value = entity.ToDto()
				};
			}
			catch (NullReferenceException ex)
			{
				return new Response<RatingDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Запись не найдена"
				};
			}
			catch (Exception ex)
			{
				return new Response<RatingDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response<IEnumerable<RatingDto>>> GetAll()
		{
			try
			{
				var list = await repos.GetAllAsync();
				if (list == null)
					return new Response<IEnumerable<RatingDto>>()
					{
						IsSuccess = true,
						Value = null
					};
				else
					return new Response<IEnumerable<RatingDto>>()
					{
						IsSuccess = true,
						Value = list.Select(e => e.ToDto())
					};
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<RatingDto>>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response> Update(RatingDto RatingDto)
		{
			try
			{
				await repos.UpdateAsync(RatingDto.ToEntity());
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
