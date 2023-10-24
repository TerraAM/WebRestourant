using System;
using System.Collections.Generic;
using System.Data;
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
    public class OrderStatusController : Controller
    {
        private readonly OrderStatusInteractor interactor;

        public OrderStatusController(OrderStatusInteractor interactor)
        {
            this.interactor = interactor;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
            var response = await interactor.GetAll();
            return View(response.Value);
        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // GET: OrderStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] OrderStatusDto orderStatus)
        {
            if (ModelState.IsValid)
            {
                var response = await interactor.Create(orderStatus);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] OrderStatusDto orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await interactor.Update(orderStatus);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await interactor.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
