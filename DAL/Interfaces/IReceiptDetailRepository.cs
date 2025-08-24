using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReceiptDetailRepository : IRepository<ReceiptDetail>
    {
        Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync();
    }
}
