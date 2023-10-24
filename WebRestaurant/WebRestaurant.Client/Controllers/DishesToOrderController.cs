using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Client.Controllers
{
    public class DishesToOrderController : Controller
    {
        private readonly DishesToOrderInteractor interactor;
        private readonly OrderInteractor orderInteractor;
        private readonly DishInteractor dishInteractor;

        public DishesToOrderController(DishesToOrderInteractor interactor, OrderInteractor orderInteractor, DishInteractor dishInteractor)
        {
            this.interactor = interactor;
            this.orderInteractor = orderInteractor;
            this.dishInteractor = dishInteractor;
        }

        // GET: DishesToOrder
        public async Task<IActionResult> Index()
        {
            var response = await interactor.GetAll();
            return View(response.Value);
        }

        // GET: DishesToOrder/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // GET: DishesToOrder/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["OrderId"] = new SelectList(orderInteractor.GetAll().Result.Value, "Id", "Id");
            return View();
        }

        // POST: DishesToOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DishId,OrderId,Amount")] DishesToOrderDto dishesToOrder)
        {
            if (ModelState.IsValid)
            {
                var response = await interactor.Create(dishesToOrder);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["OrderId"] = new SelectList(orderInteractor.GetAll().Result.Value, "Id", "Id");
            return View(dishesToOrder);
        }

        // GET: DishesToOrder/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await interactor.GetById(id);
            if (response.IsSuccess)
            {
                ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
                ViewData["OrderId"] = new SelectList(orderInteractor.GetAll().Result.Value, "Id", "Id");
                return View(response.Value);
            }
            return NotFound();
        }

        // POST: DishesToOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DishId,OrderId,Amount")] DishesToOrderDto dishesToOrder)
        {
            if (id != dishesToOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await interactor.Update(dishesToOrder);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["OrderId"] = new SelectList(orderInteractor.GetAll().Result.Value, "Id", "Id");
            return View(dishesToOrder);
        }

        // GET: DishesToOrder/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: DishesToOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await interactor.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
