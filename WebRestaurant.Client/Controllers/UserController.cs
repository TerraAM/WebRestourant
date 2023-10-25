using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRestaurant.Adapter.Services;
using WebRestaurant.App.Interactors;
using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.Client.Controllers
{
	[Authorize(Roles = "admin")]
	public class UserController : Controller
    {
        private readonly UserInteractor interactor;
        private readonly RoleInteractor roleInteractor;

        public UserController(UserInteractor interactor, RoleInteractor roleInteractor)
        {
            this.interactor = interactor;
            this.roleInteractor = roleInteractor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var response = await interactor.GetAll();
            return View(response.Value);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value): NotFound();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(roleInteractor.GetAll().Result.Value, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,RoleId")] UserDto user)
        {
            if (ModelState.IsValid)
            {
                var response = await interactor.Create(user);
                if (response.IsSuccess)
                { 
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RoleId"] = new SelectList(roleInteractor.GetAll().Result.Value, "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await interactor.GetById(id);
            if (response.IsSuccess)
            {
                ViewData["RoleId"] = new SelectList(roleInteractor.GetAll().Result.Value, "Id", "Name", response.Value.RoleId);
                return View(response.Value);
            }
            return NotFound();          
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,RoleId")] UserDto user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await interactor.Update(user);
                if (response.IsSuccess) 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RoleId"] = new SelectList(roleInteractor.GetAll().Result.Value, "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value): NotFound();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await interactor.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
