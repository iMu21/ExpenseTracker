using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpenseTracker.Controllers
{
    [AllowAnonymous]
    public class ExpensesController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;

        public ExpensesController(IHttpContextAccessor contextAccessor,
                                  IExpenseService expenseService,
                                  ICategoryService categoryService)
        {
            _contextAccessor = contextAccessor;
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _expenseService.GetAllAsync();

            return View(expenses);
        }

        public async Task<IActionResult> Details(long? id)
        {
            var expense = await _expenseService.FIndByIdAsync(id ?? 0);

            if (expense == null)
                return RedirectToAction(nameof(Index));

            return View(expense);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            Expense expense = new Expense();
            expense.Categories = categories;
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,ExpenseDateTimeUtc,CategoryId")] Expense expense)
        {
            if (expense.ExpenseDateTimeUtc > DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(expense.ExpenseDateTimeUtc), "Date should not be future date");
                return await Create();
            }
            expense = await _expenseService.AddAsync(expense);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var expense = await _expenseService.FIndByIdAsync(id);

            if (expense == null)
                return RedirectToAction(nameof(Index));

            expense.Categories = await _categoryService.GetAllAsync();

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Amount,ExpenseDateTimeUtc,CategoryId")] Expense expense)
        {
            if (expense.ExpenseDateTimeUtc > DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(expense.ExpenseDateTimeUtc), "Date should not be future date");
                return await Edit(id);
            }
            var entity = await _expenseService.FIndByIdAsync(id);
            if (entity != null)
            {
                entity.Amount = expense.Amount;
                entity.ExpenseDateTimeUtc = expense.ExpenseDateTimeUtc;
                entity.CategoryId = expense.CategoryId;
                await _expenseService.EditAsync(id, entity);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var expense = await _expenseService.FIndByIdAsync(id ?? 0);

            if (expense == null)
                return RedirectToAction(nameof(Index));

            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _expenseService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
