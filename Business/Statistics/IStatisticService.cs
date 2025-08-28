using Business.Products.DTOs;

namespace Business.Statistics
{
    public interface IStatisticService
    {
        Task<IEnumerable<ProductReadDto>> GetMostPopularProductsAsync(int productCount);

        Task<IEnumerable<ProductReadDto>> GetCustomersMostPopularProductsAsync(int productCount, int customerId);

        Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate, DateTime endDate);

        Task<decimal> GetIncomeOfCategoryInPeriod(int categoryId, DateTime startDate, DateTime endDate);
    }
}
