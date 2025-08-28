using MediatR;

namespace Business.Products.Commands
{
    public class RemoveCategoryAsyncCommand : IRequest
    {
        public int CategoryId { get; set; }
    }
}
