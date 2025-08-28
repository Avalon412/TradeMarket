using Business.Products.DTOs;
using DAL.Interfaces;
using MediatR;

namespace Business.Products.Queries.Handlers
{
    public class GetByFilterAsyncHandler : IRequestHandler<GetByFilterAsyncQuerie, IEnumerable<ProductReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByFilterAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReadDto>> Handle(GetByFilterAsyncQuerie request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithDetailsAsync();

            var filteredProducts = products
                .Where(x => !request.Filter.CategoryId.HasValue || x.ProductCategoryId == request.Filter.CategoryId)
                .Where(x => !request.Filter.MinPrice.HasValue || x.Price >= request.Filter.MinPrice)
                .Where(x => !request.Filter.MaxPrice.HasValue || x.Price <= request.Filter.MaxPrice);

            var filteredProductsDtos = new List<ProductReadDto>();

            foreach (var product in filteredProducts)
            {
                var productDto = new ProductReadDto
                {
                    Id = product.Id,
                    ProductCategoryId = product.ProductCategoryId,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    Price = product.Price,
                    ReceiptDetailIds = product.ReceiptDetails.Select(x => x.Id).ToList()
                };

                filteredProductsDtos.Add(productDto);
            }

            return filteredProductsDtos;
        }
    }
}
