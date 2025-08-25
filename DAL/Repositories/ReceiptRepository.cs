using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(TradeMarketDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(x => x.ReceiptDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Person)
                .ToListAsync();
        }

        public async Task<Receipt?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet.Include(x => x.ReceiptDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Person)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
