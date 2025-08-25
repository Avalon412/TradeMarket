using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TradeMarketDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(x => x.Person)
                .Include(x => x.Receipts)
                .ThenInclude(x => x.ReceiptDetails)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet.Include(x => x.Person)
                .Include(x => x.Receipts)
                .ThenInclude(x => x.ReceiptDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
