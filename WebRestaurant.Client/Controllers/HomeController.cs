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
		private readonly RatingInteractor ratingInteractor;
		private readonly FeedBackInteractor feedBackInteractor;

		public HomeController(DishInteractor interactor, CommentInteractor commentInteractor, UserInteractor userInteractor, RatingInteractor ratingInteractor, FeedBackInteractor feedBackInteractor)
        {
			this.interactor = interactor;
			this.commentInteractor = commentInteractor;
			this.userInteractor = userInteractor;
			this.ratingInteractor = ratingInteractor;
			this.feedBackInteractor = feedBackInteractor;
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
				var ratings = await ratingInteractor.GetAll();
				if (comments.Value.Any())
					dishModel.Comments = comments.Value.Where(x => x.DishId == id).ToList();
				if (ratings.Value.Any())
				{
					dishModel.Rating = ratings.Value.Where(x => x.DishId == id).Select(x=>x.Rate).Average();
				}
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
		public IActionResult FeedBack()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> FeedBack([Bind("Id,Email,Title,Context,CreateTime")] FeedBackDto FeedBackDto)
		{
			if (ModelState.IsValid)
			{
				var response = await feedBackInteractor.Create(FeedBackDto);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(FeedBackDto);
		}

		public async Task<IActionResult> ChangeRating(int modalRating, int DishId)
		{
			var user = userInteractor.GetAll().Result.Value.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
			var rating = ratingInteractor.GetAll().Result.Value.Where(x => x.DishId == DishId && x.UserId == user.Id).FirstOrDefault();
			if (rating != null)
			{
				await ratingInteractor.Delete(rating.Id);
			}
			var newRating = new RatingDto() 
			{
				Rate = modalRating,
				UserId = user.Id,
				DishId = DishId,
			};
			await ratingInteractor.Create(newRating);
			return RedirectToAction("Details", new { id = DishId});
		}
	}
}
