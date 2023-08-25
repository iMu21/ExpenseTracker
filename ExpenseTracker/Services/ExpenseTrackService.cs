using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Vm;

namespace ExpenseTracker.Services
{
    public class ExpenseTrackService : IExpenseTrackService
    {
        private readonly IExpenseService _expenseService;
        public ExpenseTrackService(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public async Task<List<ClusteredExpenseVm>> TrackExpenseAsync(DateTime fromDateTimeUtc, DateTime toDateTimeUtc)
        {
            var expenses = (await _expenseService.GetAllAsync()).Where(e=>e.ExpenseDateTimeUtc>=fromDateTimeUtc&&e.ExpenseDateTimeUtc<=toDateTimeUtc).ToList();
            ExpenseReportVm trackingReport = new ExpenseReportVm(fromDateTimeUtc, toDateTimeUtc, expenses);
            return trackingReport.GetClusteredExpenses();
        }
    }
}
