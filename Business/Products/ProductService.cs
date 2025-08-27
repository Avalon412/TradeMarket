using Business.Customers.DTOs;
using Business.Products.DTOs;
using DAL.Entities;
using DAL.Interfaces;

namespace Business.Products
{
    public class ProductService : IProductInterface
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithDetailsAsync();

            var productDtos = new List<ProductReadDto>();

            foreach (var product in products)
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

        public async Task<ProductReadDto?> GetAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);

            if (product is not null)
            {
                return new ProductReadDto
                {
                    Id = product.Id,
                    ProductCategoryId = product.Category.Id,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    Price = product.Price,
                    ReceiptDetailIds = product.ReceiptDetails.Select(x => x.Id).ToList()
                };
            }

            return new ProductReadDto();
        }

        public async Task AddAsync(ProductWriteDto product)
        {
            var entity = new Product
            {
                ProductName = product.ProductName,
                ProductCategoryId = product.ProductCategoryId,
                Price = product.Price,
            };

            _unitOfWork.ProductRepository.Add(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ProductWriteDto product)
        {
            var entity = new Product
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductCategoryId = product.ProductCategoryId,
                Price = product.Price,
            };

            _unitOfWork.ProductRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.ProductRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
