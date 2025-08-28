using Business.Products.DTOs;
using MediatR;

namespace Business.Products.Commands
{
    public class AddCategoryAsyncCommand : IRequest
    {
        public required ProductCategoryDto ProductCategoryDto { get; set; }
    }
}
