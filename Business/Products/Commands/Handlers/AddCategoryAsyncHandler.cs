using DAL.Entities;
using DAL.Interfaces;
using MediatR;

namespace Business.Products.Commands.Handlers
{
    public class AddCategoryAsyncHandler : IRequestHandler<AddCategoryAsyncCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddCategoryAsyncCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProductCategory
            {
                CategoryName = request.ProductCategoryDto.CategoryName
            };

            _unitOfWork.ProductCategoryRepository.Add(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
