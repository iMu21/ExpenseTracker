using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface IBaseService<TEntity> where TEntity:Auditable
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> FIndByIdAsync(long id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?> EditAsync(long Id,TEntity entity);
        Task DeleteAsync(long Id);
    }
}
