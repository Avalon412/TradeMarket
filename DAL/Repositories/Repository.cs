using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TradeMarketDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(TEntity entity)
        {
            if (entity is not null)
                _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity is not null)
                _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                var entity = await GetByIdAsync(id);
                if (entity is not null)
                {
                    Delete(entity);
                }
            }
        }
    }
}
