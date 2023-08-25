using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context,IHttpContextAccessor httpContext) : base(context, httpContext) { }
    }
}
