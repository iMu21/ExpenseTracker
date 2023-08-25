using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Auditable
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public BaseService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> entities;
            if (typeof(TEntity) == typeof(Expense))
            {
                entities= await _context.Set<TEntity>()
                    .Include("Category")
                    .ToListAsync();
            }
            else
            {
                entities= await _context.Set<TEntity>()
                                .ToListAsync();
            }

            return entities == null ? new List<TEntity>() { } : entities;
        }
        public async Task<TEntity?> FIndByIdAsync(long id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedBy = GetUserId();
            entity.CreatedDateUtc = DateTime.UtcNow;

            _context.Add(entity);

            await _context.SaveChangesAsync();

            return entity;

        }
        public async Task DeleteAsync(long Id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(Id);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<TEntity?> EditAsync(long Id, TEntity entity)
        {
            if (entity == null|| entity.Id!=Id)
            {
                return null;
            }

            entity.UpdatedBy = GetUserId();
            entity.UpdatedDateUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return entity;
        }
        private string? GetUserId()
        {
            return _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
