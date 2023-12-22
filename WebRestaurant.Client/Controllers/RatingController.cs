using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Client.Controllers
{
	[Authorize(Roles = "admin")]
	public class RatingController : Controller
    {
		private readonly RatingInteractor interactor;
		private readonly UserInteractor userInteractor;
		private readonly DishInteractor dishInteractor;

		public RatingController(RatingInteractor interactor, UserInteractor userInteractor, DishInteractor dishInteractor)
		{
			this.interactor = interactor;
			this.userInteractor = userInteractor;
			this.dishInteractor = dishInteractor;
		}

		// GET: Rating
		public async Task<IActionResult> Index()
        {
			var response = await interactor.GetAll();
			return View(response.Value);
		}

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int id)
        {
			var response = await interactor.GetById(id);
			return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
			ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
			ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
			return View();
		}

        // POST: Rating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rate,UserId,DishId")] RatingDto RatingDto)
        {
			if (ModelState.IsValid)
			{
				var response = await interactor.Create(RatingDto);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
			ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
			return View(RatingDto);
		}

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
			var response = await interactor.GetById(id);
			if (response.IsSuccess)
			{
				ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
				ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
				return View(response.Value);
			}
			return NotFound();
		}

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rate,UserId,DishId")] RatingDto RatingDto)
        {
			if (id != RatingDto.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var response = await interactor.Update(RatingDto);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
			ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
			return View(RatingDto);
		}

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
			var response = await interactor.GetById(id);
			return response.IsSuccess ? View(response.Value) : NotFound();
		}

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			await interactor.Delete(id);
			return RedirectToAction(nameof(Index));
		}
    }
}
