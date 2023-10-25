using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;
using WebRestaurant.Shared.Model;

namespace WebRestaurant.Client.Controllers
{
	public class StatisticController : Controller
	{
		private readonly DishInteractor dishInteractor;
		private readonly DishesToOrderInteractor dishesToOrderInteractor;
		public StatisticController(DishInteractor dishInteractor, DishesToOrderInteractor dishesToOrderInteractor)
		{
			this.dishInteractor = dishInteractor;
			this.dishesToOrderInteractor = dishesToOrderInteractor;
		}
		// GET: StatisticController
		public ActionResult Index()
		{
			// Здесь вы можете получить данные для диаграммы из источника данных (например, базы данных или сервиса)
			var dishes = dishInteractor.GetAll().Result.Value.ToList();
			var dishesToOrder = dishesToOrderInteractor.GetAll().Result.Value.ToList();

			if (!dishes.Any())
			{
				dishes = new List<DishDto>();
			}
			if (!dishesToOrder.Any())
			{
				dishesToOrder = new List<DishesToOrderDto>();
			}

			string[] Labels = dishes.Select(x => x.Name).ToArray();

			List<int> tempData = new List<int>();
			foreach (var el in dishes)
			{
				tempData.Add(dishesToOrder.Where(x => x.DishId == el.Id).Sum(d => d.Amount));
			}

			int[] Data = tempData.ToArray();

			var model = new StatisticalModel
			{
				Data = Data,
				Labels = Labels
			};

			return View(model);
		}
	}
}
