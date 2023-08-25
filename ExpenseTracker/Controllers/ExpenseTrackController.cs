using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Vm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseTrackController : Controller
    {
        private readonly IExpenseTrackService _expenseTrackService;
        public ExpenseTrackController(IExpenseTrackService expenseTrackService)
        {
            _expenseTrackService = expenseTrackService;
        }
        public ActionResult Index()
        {
            return View();
        }

        

        // POST: ExpenseTrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Track([Bind("FromDate,ToDate")] ExpenseReportVm expenseReportVm)
        {
            var clusteredExpenses = await _expenseTrackService.TrackExpenseAsync(expenseReportVm.FromDate, expenseReportVm.ToDate);
            
             return View(clusteredExpenses);
            
        }
    }
}
