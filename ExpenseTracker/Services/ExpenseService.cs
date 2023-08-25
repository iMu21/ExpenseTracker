using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        public ExpenseService(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext) { }
    }
}
