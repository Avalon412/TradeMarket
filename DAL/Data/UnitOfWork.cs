using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly TradeMarketDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(TradeMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IPersonRepository PersonRepository => new PersonRepository(_dbContext);

        public ICustomerRepository CustomerRepository => new CustomerRepository(_dbContext);

        public IProductRepository ProductRepository => new ProductRepository(_dbContext);

        public IProductCategoryRepository ProductCategoryRepository => new ProductCategoryRepository(_dbContext);

        public IReceiptRepository ReceiptRepository => new ReceiptRepository(_dbContext);

        public IReceiptDetailRepository ReceiptDetailRepository => new ReceiptDetailRepository(_dbContext);

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _dbContext.Dispose();

            _disposed = true;
        }
    }
}
