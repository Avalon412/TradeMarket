using Business.Products.DTOs;
using DAL.Interfaces;
using MediatR;

namespace Business.Products.Queries.Handlers
{
    public class GetAllProductCategoriesAsyncHandler : IRequestHandler<GetAllProductCategoriesAsyncQuerie, IEnumerable<ProductCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductCategoriesAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductCategoryDto>> Handle(GetAllProductCategoriesAsyncQuerie request, CancellationToken cancellationToken)
        {
            var productCategories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();

            var productCategoriesDtos = new List<ProductCategoryDto>();

            foreach (var category in productCategories)
            {
                var categoryDto = new ProductCategoryDto
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                };

                productCategoriesDtos.Add(categoryDto);
            }

            return productCategoriesDtos;
        }
    }
}
