using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
        void Update(TEntity entity);
    }
}
