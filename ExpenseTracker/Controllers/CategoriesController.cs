using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Controllers
{
    [AllowAnonymous]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoriesController(ICategoryService categoryService, IHttpContextAccessor contextAccessor)
        {
            _categoryService = categoryService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(long? id)
        {
            var category = await _categoryService.FIndByIdAsync(id??0);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            var isExist = await _categoryService.IsExistAsync(category.Name);

            if (isExist)
                ModelState.AddModelError(nameof(category.Name), "Category already exist");

            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            var category = await _categoryService.FIndByIdAsync(id??0);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name")] Category category)
        {
            var isNameExist = await _categoryService.IsExistAsync(category.Name);

            if (isNameExist)
                ModelState.AddModelError(nameof(category.Name), "Category already exist");

            if (ModelState.IsValid)
            {
                var entity = await _categoryService.FIndByIdAsync(id);
                if (entity != null)
                {
                    entity.Name = category.Name;
                    await _categoryService.EditAsync(id, entity);
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(nameof(category.Name), "Doesn't exist");
            return View(category);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var category = await _categoryService.FIndByIdAsync(id ?? 0);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
