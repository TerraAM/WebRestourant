using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderInteractor interactor;
        private readonly UserInteractor userInteractor;
        private readonly DinnerTableInteractor dinnerTableInteractor;
        private readonly OrderStatusInteractor statusInteractor;

        public OrderController(OrderInteractor interactor, UserInteractor userInteractor, DinnerTableInteractor dinnerTableInteractor, OrderStatusInteractor statusInteractor)
        {
            this.interactor = interactor;
            this.userInteractor = userInteractor;
            this.dinnerTableInteractor = dinnerTableInteractor;
            this.statusInteractor = statusInteractor;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var response = await interactor.GetAll();
            return View(response.Value);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
            ViewData["StatusId"] = new SelectList(statusInteractor.GetAll().Result.Value, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DinnerTableId,ClientId,DateCreate,Duration,Price,StatusId")] OrderDto order)
        {
            if (ModelState.IsValid)
            {
                var response = await interactor.Create(order);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ClientId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
            ViewData["StatusId"] = new SelectList(statusInteractor.GetAll().Result.Value, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await interactor.GetById(id);
            if (response.IsSuccess)
            {
                ViewData["ClientId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
                ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
                ViewData["StatusId"] = new SelectList(statusInteractor.GetAll().Result.Value, "Id", "Name");
                return View(response.Value);
            }
            return NotFound();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DinnerTableId,ClientId,DateCreate,Duration,Price,StatusId")] OrderDto order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await interactor.Update(order);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ClientId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["DinnerTableId"] = new SelectList(dinnerTableInteractor.GetAll().Result.Value, "Id", "Number");
            ViewData["StatusId"] = new SelectList(statusInteractor.GetAll().Result.Value, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await interactor.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
