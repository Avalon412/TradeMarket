using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ReceiptDetailRepository : Repository<ReceiptDetail>, IReceiptDetailRepository
    {
        public ReceiptDetailRepository(TradeMarketDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(x => x.Receipt)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .ToListAsync();
        }
    }
}
