using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Client.Services;
using WebRestaurant.Domain.Data;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;
using WebRestaurant.Shared.Model;

namespace WebRestaurant.Client.Controllers
{
    public class HomeController : Controller
    {
		private readonly DishInteractor interactor;
		private readonly CommentInteractor commentInteractor;
		private readonly UserInteractor userInteractor;

		public HomeController(DishInteractor interactor, CommentInteractor commentInteractor, UserInteractor userInteractor)
        {
			this.interactor = interactor;
			this.commentInteractor = commentInteractor;
			this.userInteractor = userInteractor;
		}
        // GET: HomeController
        public async Task<IActionResult> Index()
        {
			var response = await interactor.GetAll();
			return View(response.Value);
		}

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
			var response = await interactor.GetById(id);
			if (response.IsSuccess == true)
			{
				var dishModel = new DishModel();
				dishModel.Dish = response.Value;
				var comments = await commentInteractor.GetAll();
				if (comments.Value.Any())
					dishModel.Comments = comments.Value.Where(x => x.DishId == id).ToList();
				return View(dishModel);
			}
			return NotFound();
		}

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            List<PreOrder> preOrders = HttpContext.Session.GetObjectFromJson<List<PreOrder>>("preOrdersList");

            if (preOrders == null)
            {
                // Если список еще не существует, создаем новый список
                preOrders = new List<PreOrder>();
            }

            // Создание нового объекта
            PreOrder newPreOrder = new PreOrder { DishId = productId, Amount = quantity };

            // Добавление нового объекта к списку
            preOrders.Add(newPreOrder);

            // Сохранение списка в сеансовом состоянии
            HttpContext.Session.SetObjectAsJson("preOrdersList", preOrders);
            // Логика добавления товара в корзину

            return Json(new { success = true });
        }
		[HttpPost]
		public async Task<IActionResult> Details(string AuthorEmail, int DishId, string Text)
		{
			if (DishId == null)
			{
				return NotFound();
			}

			var author = userInteractor.GetAll().Result.Value.Where(x => x.Email == AuthorEmail).FirstOrDefault();

			var newComment = new CommentDto()
			{
				Content = Text,
				UserId = author.Id,
				DishId = DishId,
				CreatedDate = DateTime.Now

			};

			await commentInteractor.Create(newComment);

			var dishModel = new DishModel();
			dishModel.Dish = interactor.GetById(DishId).Result.Value;
			dishModel.Comments = commentInteractor.GetAll().Result.Value.Where(x => x.DishId == DishId).ToList();
			return View(dishModel);
		}
	}
}
