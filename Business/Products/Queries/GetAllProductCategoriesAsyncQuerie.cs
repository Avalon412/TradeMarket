using Business.Products.DTOs;
using MediatR;

namespace Business.Products.Queries
{
    public class GetAllProductCategoriesAsyncQuerie : IRequest<IEnumerable<ProductCategoryDto>>
    {
    }
}
