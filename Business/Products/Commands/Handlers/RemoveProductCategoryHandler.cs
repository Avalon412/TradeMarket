using DAL.Interfaces;
using MediatR;

namespace Business.Products.Commands.Handlers
{
    public class RemoveProductCategoryHandler : IRequestHandler<RemoveCategoryAsyncCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveCategoryAsyncCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductCategoryRepository.DeleteByIdAsync(request.CategoryId);
            await _unitOfWork.SaveAsync();
        }
    }
}
