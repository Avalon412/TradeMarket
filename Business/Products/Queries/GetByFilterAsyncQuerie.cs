using Business.Products.DTOs;
using MediatR;

namespace Business.Products.Queries
{
    public class GetByFilterAsyncQuerie : IRequest<IEnumerable<ProductReadDto>>
    {
        public required FilterSearchDto Filter { get; set; }
    }
}
