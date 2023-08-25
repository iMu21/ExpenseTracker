using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface ICategoryService:IBaseService<Category>
    {
        Task<bool> IsExistAsync(string name);
    }
}
