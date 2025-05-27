using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Repository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly WorkflowDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(WorkflowDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public virtual async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TEntity entity) => _dbSet.Update(entity);
        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}
