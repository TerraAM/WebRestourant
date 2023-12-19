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
using WebRestaurant.Entity.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Client.Controllers
{
    public class CommentController : Controller
    {
		private readonly CommentInteractor interactor;
		private readonly UserInteractor userInteractor;
		private readonly DishInteractor dishInteractor;

		public CommentController(CommentInteractor interactor, UserInteractor userInteractor, DishInteractor dishInteractor)
        {
			this.interactor = interactor;
			this.userInteractor = userInteractor;
			this.dishInteractor = dishInteractor;
		}

        // GET: Comment
        public async Task<IActionResult> Index()
        {
			var response = await interactor.GetAll();
			return View(response.Value);
		}

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int id)
        {
			var response = await interactor.GetById(id);
			return response.IsSuccess ? View(response.Value) : NotFound();
		}

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
            ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,UserId,DishId,CreatedDate")] CommentDto CommentDto)
        {
			if (ModelState.IsValid)
			{
				var response = await interactor.Create(CommentDto);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
			ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
			return View(CommentDto);
        }

        // GET: Comment/Edit/5
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

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,UserId,DishId,CreatedDate")] CommentDto CommentDto)
        {
			if (id != CommentDto.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var response = await interactor.Update(CommentDto);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			ViewData["DishId"] = new SelectList(dishInteractor.GetAll().Result.Value, "Id", "Name");
			ViewData["UserId"] = new SelectList(userInteractor.GetAll().Result.Value, "Id", "Name");
			return View(CommentDto);
		}

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
			var response = await interactor.GetById(id);
			return response.IsSuccess ? View(response.Value) : NotFound();
		}

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			await interactor.Delete(id);
			return RedirectToAction(nameof(Index));
		}
    }
}
