using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Mappers;
using WebRestaurant.Client.Services;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;
using WebRestaurant.Shared.Model;

namespace WebRestaurant.Client.Controllers
{
    public class PreOrdersController : Controller
    {
        private readonly WebDbContext _context;

        public PreOrdersController(WebDbContext context)
        {
            _context = context;
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
                    preOrders[i].Dish = _context.Dishes.FirstOrDefault(x => x.Id == preOrders[i].DishId).ToDto();
                }
            }
            ViewData["DinnerTableId"] = new SelectList(_context.DinnerTables, "Id", "Number");
            return View(preOrders);
        }

        // POST: PreOrders
        [HttpPost]
        public IActionResult Pricol(int DinnerTableId, int ServiceManId)
        {
            List<PreOrder> preOrders = HttpContext.Session.GetObjectFromJson<List<PreOrder>>("preOrdersList");
            if (preOrders == null)
            {
                // Если список еще не существует, создаем новый список
                preOrders = new List<PreOrder>();
                return RedirectToAction(nameof(Index));
            }

            User client = new User();

            if (!User.Identity.IsAuthenticated)
            {
                client = _context.Users.FirstOrDefault();
            }
            else
            {
                client = _context.Users.FirstOrDefault(x=>x.Email == User.Identity.Name);
            }

            List<DishesToOrder> newapplicationOrders = new List<DishesToOrder>();
            DishDto tempDish;
            double totalPrice = 0;
            OrderDto newOrder = new OrderDto()
            {
                DinnerTableId = DinnerTableId,
                DateCreate = DateTime.Now,
                Client = client.ToDto(),
                Status = _context.OrderStatuses.FirstOrDefault().ToDto()
            };
            foreach (var preOrder in preOrders)
            {
                tempDish = _context.Dishes.FirstOrDefault(x => x.Id == preOrder.DishId).ToDto();
                newapplicationOrders.Add(new DishesToOrder() 
                {
                    Dish = tempDish.ToEntity(),
                    Order = newOrder.ToEntity(),
                    Amount = preOrder.Amount
                });
                totalPrice += tempDish.Price * preOrder.Amount;
            }
            newOrder.Price = totalPrice;
            _context.Add(newOrder.ToEntity());
            _context.AddRange(newapplicationOrders);
            _context.SaveChanges();
            HttpContext.Session.Clear();
            ViewData["DinnerTableId"] = new SelectList(_context.DinnerTables, "Id", "Number");
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
    }
}
