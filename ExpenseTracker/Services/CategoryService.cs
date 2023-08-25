using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
            _context = context;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await _context.Category.AnyAsync(a => a.Name.ToLower() == name.ToLower());
        }
    }
}
