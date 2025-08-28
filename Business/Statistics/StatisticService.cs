using Business.Products.DTOs;
using DAL.Entities;
using DAL.Interfaces;

namespace Business.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReadDto>> GetCustomersMostPopularProductsAsync(int productCount, int customerId)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            var topProducts = receipts
                .Where(x => x.CustomerId == customerId)
                .SelectMany(r => r.ReceiptDetails)
                .GroupBy(rd => rd.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalQuantity = g.Sum(rd => rd.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(productCount)
                .Select(x => x.Product);

            var productDtos = new List<ProductReadDto>();

            foreach (var product in topProducts)
            {
                var productDto = new ProductReadDto
                {
                    Id = product.Id,
                    ProductCategoryId = product.Category.Id,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    Price = product.Price,
                    ReceiptDetailIds = product.ReceiptDetails.Select(x => x.Id).ToList()
                };

                productDtos.Add(productDto);
            }

            return productDtos;
        }

        public async Task<decimal> GetIncomeOfCategoryInPeriod(int categoryId, DateTime startDate, DateTime endDate)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            return receipts
                .Where(r => r.OperationDate >= startDate && r.OperationDate <= endDate)
                .SelectMany(r => r.ReceiptDetails)
                .Where(rd => rd.Product.ProductCategoryId == categoryId)
                .Sum(rd => rd.Quantity * rd.DiscountUnitPrice);
        }

        public async Task<IEnumerable<ProductReadDto>> GetMostPopularProductsAsync(int productCount)
        {
            var receiptDetails = await _unitOfWork.ReceiptDetailRepository.GetAllWithDetailsAsync();

            var topProducts = receiptDetails
                .GroupBy(rd => rd.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalQuantity = g.Sum(rd => rd.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(productCount)
                .Select(x => x.Product);

            var productDtos = new List<ProductReadDto>();

            foreach (var product in topProducts)
            {
                var productDto = new ProductReadDto
                {
                    Id = product.Id,
                    ProductCategoryId = product.Category.Id,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    Price = product.Price,
                    ReceiptDetailIds = product.ReceiptDetails.Select(x => x.Id).ToList()
                };

                productDtos.Add(productDto);
            }

            return productDtos;
        }

        public async Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate, DateTime endDate)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            return receipts
                .Where(r => r.OperationDate >= startDate && r.OperationDate <= endDate)
                .GroupBy(rd => rd.Customer)
                .Select(g => new
                {
                    Customer = g.Key,
                    ReceiptSum = g
                        .SelectMany(r => r.ReceiptDetails)
                        .Sum(rd => rd.Quantity * rd.DiscountUnitPrice)
                })
                .OrderByDescending(x => x.ReceiptSum)
                .Take(customerCount)
                .Select(x => new CustomerActivityDto
                {
                    CustomerId = x.Customer.Id,
                    CustomerName = x.Customer.Person.Name + " " + x.Customer.Person.Surname,
                    ReceiptSum = x.ReceiptSum
                });
        }
    }
}
