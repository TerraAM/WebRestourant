using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRestaurant.App.Data;
using WebRestaurant.App.Mappers;
using WebRestaurant.Domain.Data;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Interactors
{
	public class CommentInteractor
	{
		private IRepository<Comment> repos;
		private IUnitWork unitWork;

		public CommentInteractor(IRepository<Comment> repos, IUnitWork unitWork)
		{
			this.repos = repos;
			this.unitWork = unitWork;
		}
		public async Task<Response> Create(CommentDto CommentDto)
		{
			var response = new Response<CommentDto>();
			try
			{
				await repos.CreateAsync(CommentDto.ToEntity());
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
		public async Task<Response<CommentDto>> GetById(int id)
		{
			try
			{
				var entity = await repos.GetByIdAsync(id);
				return new Response<CommentDto>()
				{
					IsSuccess = true,
					Value = entity.ToDto()
				};
			}
			catch (NullReferenceException ex)
			{
				return new Response<CommentDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Запись не найдена"
				};
			}
			catch (Exception ex)
			{
				return new Response<CommentDto>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response<IEnumerable<CommentDto>>> GetAll()
		{
			try
			{
				var list = await repos.GetAllAsync();
				if (list == null)
					return new Response<IEnumerable<CommentDto>>()
					{
						IsSuccess = true,
						Value = null
					};
				else
					return new Response<IEnumerable<CommentDto>>()
					{
						IsSuccess = true,
						Value = list.Select(e => e.ToDto())
					};
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<CommentDto>>()
				{
					IsSuccess = false,
					ErrorInfo = ex.Message,
					ErrorMessage = "Ошибка получения"
				};
			}
		}

		public async Task<Response> Update(CommentDto CommentDto)
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
