using Business.Customers.DTOs;
using Business.Products.DTOs;
using Business.Shared;
using DAL.Entities;
using DAL.Interfaces;

namespace Business.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private const string CONTAINER_NAME = "products";

        public ProductService(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
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
                    ImageUrl = product.ImageUrl,
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
                    ImageUrl = product.ImageUrl,
                    ReceiptDetailIds = product.ReceiptDetails.Select(x => x.Id).ToList()
                };
            }

            return new ProductReadDto() { ReceiptDetailIds = new List<int>() };
        }

        public async Task AddAsync(ProductWriteDto product)
        {
            var entity = new Product
            {
                ProductName = product.ProductName,
                ProductCategoryId = product.ProductCategoryId,
                Price = product.Price,
            };

            if (product.Picture is not null)
            {
                entity.ImageUrl = await _fileStorageService.SaveFile(CONTAINER_NAME, product.Picture);
            }

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
