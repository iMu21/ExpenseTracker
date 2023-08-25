using ExpenseTracker.Vm;

namespace ExpenseTracker.Models
{
    public class ExpenseReportVm
    {
        public ExpenseReportVm()
        {
        }
        public ExpenseReportVm(DateTime fromDate, DateTime toDate, List<Expense> trackedExpenses)
        {
            FromDate = fromDate;
            ToDate = toDate;
            Expenses = trackedExpenses;
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        private List<Expense> Expenses { get; set; }
        public List<ClusteredExpenseVm> GetClusteredExpenses()
        {
            var trackedClusteredExpenses = new List<ClusteredExpenseVm> { };

            var categories = Expenses.Select(e => e.Category).Distinct().ToList();

            foreach(var category in categories)
            {
                var totalExpenseAmount = Expenses.Where(e => e.Category == category).Select(e => e.Amount).Sum();
                trackedClusteredExpenses.Add(
                    new ClusteredExpenseVm { Amount = totalExpenseAmount,Category = category}
                    );
            }

            return trackedClusteredExpenses;
        }
    }
}
