using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Academia.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AcademiaDbContext _context;
        public BaseRepository(AcademiaDbContext context) { _context = context; }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync() => await _context.SaveChangesAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(p => p.Status && p.Id == id);

        public async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<(ICollection<TResult> Result, int TotalCount)> ListAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            int pageNumber = 1, int pageSize = 10)
        {
            var result = await _context.Set<TEntity>()
                .Where(predicate).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(selector).ToListAsync();
            var total = await _context.Set<TEntity>().Where(predicate).CountAsync();
            return (result, total);
        }

        public async Task DeleteAsync(int id)
            => await _context.Set<TEntity>().Where(p => p.Id == id)
                .ExecuteUpdateAsync(p => p.SetProperty(e => e.Status, false));
    }
}