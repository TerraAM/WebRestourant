using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.App.Mappers;
using WebRestaurant.Client.Services;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;
using WebRestaurant.Shared.Model;

namespace WebRestaurant.Client.Controllers
{
    public class PreOrdersController : Controller
    {
        private readonly UserInteractor userInteractor;
		private readonly DishInteractor dishInteractor;
		private readonly DinnerTableInteractor dinnerTableInteractor;
		private readonly OrderStatusInteractor statusInteractor;
		private readonly OrderInteractor orderInteractor;
		private readonly DishesToOrderInteractor dishesToOrderInteractor;

		private readonly int[] DurationList = new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 60, 90, 120 };

		private readonly int startDayH = 8;
		private readonly int endDayH = 17;

		public PreOrdersController(UserInteractor userInteractor, DishInteractor dishInteractor, DinnerTableInteractor dinnerTableInteractor, OrderStatusInteractor statusInteractor, OrderInteractor orderInteractor, DishesToOrderInteractor dishesToOrderInteractor)
        {
            this.userInteractor = userInteractor;
			this.dishInteractor = dishInteractor;
			this.dinnerTableInteractor = dinnerTableInteractor;
			this.statusInteractor = statusInteractor;
			this.orderInteractor = orderInteractor;
			this.dishesToOrderInteractor = dishesToOrderInteractor;
		}

        // GET: PreOrders
        public ActionResult Index()
        {
            List<PreOrder> preOrders = HttpContext.Session.GetObjectFromJson<List<PreOrder>>("preOrdersList");
            if (preOrders == null)
            {
                // Если список еще не существует, создаем новый список
                preOrders = new List<PreOrder>();
            }
            else 
            {
                for (int i = 0;i < preOrders.Count;i++) 
                {
                    preOrders[i].Dish = dishInteractor.GetById(preOrders[i].DishId).Result.Value;
                }
            }
            ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
			ViewData["DurationList"] = new SelectList(DurationList);
			return View(preOrders);
        }

        // POST: PreOrders
        [HttpPost]
        public async Task<IActionResult> Pricol(int DinnerTableId, int Duration, DateTime Time)
        {
			//Если указан столик, но не указаны продолжиельность и время, то ничего не делать
			if (dinnerTableInteractor.GetById(DinnerTableId).Result.Value.Number != 0)
			{
				if (Duration <= 0 || Time < DateTime.Now)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			
			List<PreOrder> preOrders = HttpContext.Session.GetObjectFromJson<List<PreOrder>>("preOrdersList");

			//Если нет заказов и не выбран столик, то ничего не делать
			if (preOrders != null)
			{ 
				if (preOrders.Count == 0 && dinnerTableInteractor.GetById(DinnerTableId).Result.Value.Number == 0)
				{ 
					return RedirectToAction(nameof(Index));
				}
			}

			//Если указана продолжительность, но не указано время начала, то ничего не делать
			if (Duration > 0 && Time < DateTime.Now)
			{
				return RedirectToAction(nameof(Index));
			}

			//Если указана время, но не указана продолжиельность, то ничего не делать
			if (preOrders == null && Duration <= 0 && Time >= DateTime.Now)
			{
				return RedirectToAction(nameof(Index));
			}

			//Если время выбрано в нерабочее время, то ничего не делать
			if (Time.TimeOfDay.Hours > endDayH || Time.TimeOfDay.Hours < startDayH)
			{
				return RedirectToAction(nameof(Index));
			}

			//Если выбранный столик занят в это время, то ничего не делать
			if (!IsTableAvailable(DinnerTableId, Duration, Time))
			{
				return RedirectToAction(nameof(Index));
			}

			UserDto client = new UserDto();

			if (!User.Identity.IsAuthenticated)
			{
				client = userInteractor.GetAll().Result.Value.FirstOrDefault();
			}
			else
			{
				client = userInteractor.GetAll().Result.Value.FirstOrDefault(x => x.Email == User.Identity.Name);
			}

			List<DishesToOrderDto> DishesToOrder = new List<DishesToOrderDto>();
			DishDto tempDish;
			double totalPrice = 0;
			OrderDto newOrder = new OrderDto()
			{
				DateCreate = Time,
				ClientId = client.Id,
				DinnerTableId = DinnerTableId,
				Duration = Duration,
				StatusId = statusInteractor.GetAll().Result.Value.FirstOrDefault().Id
			};
			if (preOrders != null)
			{ 
				foreach (var preOrder in preOrders)
				{
					tempDish = dishInteractor.GetAll().Result.Value.FirstOrDefault(x => x.Id == preOrder.DishId);
					DishesToOrder.Add(new DishesToOrderDto()
					{
						DishId = tempDish.Id,
						Amount = preOrder.Amount
					});
					totalPrice += tempDish.Price * preOrder.Amount;
				}
				
			}

			newOrder.Price = totalPrice;
			await orderInteractor.Create(newOrder);

			var order = orderInteractor.GetAll().Result.Value.LastOrDefault();

			foreach (var dishToOrder in DishesToOrder)
			{
				dishToOrder.OrderId = order.Id;
				await dishesToOrderInteractor.Create(dishToOrder);
			}

			HttpContext.Session.Clear();

			ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
			ViewData["DurationList"] = new SelectList(DurationList);
			return RedirectToAction(nameof(Index));
        }

        // POST: PreOrders/Delete/5
        public IActionResult Delete(int id)
        {
            List<PreOrder> preOrders = HttpContext.Session.GetObjectFromJson<List<PreOrder>>("preOrdersList");
            if (preOrders == null)
            {
                // Если список еще не существует, создаем новый список
                preOrders = new List<PreOrder>();
                return RedirectToAction(nameof(Index));
            }
            preOrders.RemoveAt(id);
            HttpContext.Session.SetObjectAsJson("preOrdersList", preOrders);
            return RedirectToAction(nameof(Index));
        }

		public bool IsTableAvailable(int dinnerTableId, int duration, DateTime time)
		{
			// Получить все заказы для заданного столика
			var tableOrders = orderInteractor.GetAll().Result.Value.Where(order => order.DinnerTableId == dinnerTableId);

			// Проверить, занят ли столик в указанное время и на указанную продолжительность
			foreach (var order in tableOrders)
			{
				DateTime orderStartTime = order.DateCreate;
				DateTime orderEndTime = orderStartTime.AddMinutes(order.Duration);

				DateTime requestedEndTime = time.AddMinutes(duration);

				if (time >= orderStartTime && time < orderEndTime)
				{
					// Столик уже занят в это время
					return false;
				}

				if (requestedEndTime > orderStartTime && requestedEndTime <= orderEndTime)
				{
					// Запрошенное время перекрывается с другим заказом
					return false;
				}
			}

			// Если ни один из заказов не пересекается с запрошенным временем, столик свободен
			return true;
		}
	}
}
