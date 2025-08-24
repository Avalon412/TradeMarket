namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IReceiptRepository ReceiptRepository { get; }
        IReceiptDetailRepository ReceiptDetailRepository { get; }

        Task SaveAsync();
    }
}
