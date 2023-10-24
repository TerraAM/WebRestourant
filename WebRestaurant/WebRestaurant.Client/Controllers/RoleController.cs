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
    public class RoleController : Controller
    {
        private readonly RoleInteractor interactor;

        public RoleController(RoleInteractor interactor)
        {
            this.interactor = interactor;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var response = await interactor.GetAll();
            return View(response.Value);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RoleDto role)
        {
            if (ModelState.IsValid)
            {
                var response = await interactor.Create(role);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RoleDto role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await interactor.Update(role);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await interactor.GetById(id);
            return response.IsSuccess ? View(response.Value) : NotFound();
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await interactor.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
