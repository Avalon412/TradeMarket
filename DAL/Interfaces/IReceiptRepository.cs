using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        Task<IEnumerable<Receipt>> GetAllWithDetailsAsync();
        Task<Receipt?> GetByIdWithDetailsAsync(int id);
    }
}
