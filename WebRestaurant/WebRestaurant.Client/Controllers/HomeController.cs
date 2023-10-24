using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Client.Services;
using WebRestaurant.Shared.Model;

namespace WebRestaurant.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly DishInteractor interactor;

        public HomeController(DishInteractor interactor)
        {
			this.interactor = interactor;
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
			return response.IsSuccess ? View(response.Value) : NotFound();
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
    }
}
