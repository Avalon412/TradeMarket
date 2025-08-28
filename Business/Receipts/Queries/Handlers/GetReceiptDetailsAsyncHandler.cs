using Business.Receipts.DTOS;
using DAL.Interfaces;
using MediatR;

namespace Business.Receipts.Queries.Handlers
{
    public class GetReceiptDetailsAsyncHandler : IRequestHandler<GetReceiptDetailsAsyncQuerie, IEnumerable<ReceiptDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptDetailsAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReceiptDetailDto>> Handle(GetReceiptDetailsAsyncQuerie request, CancellationToken cancellationToken)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(request.ReceiptId);

            if (receipt is not null)
            {
                return receipt.ReceiptDetails.Select(x => new ReceiptDetailDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ReceiptId = x.ReceiptId,
                    UnitPrice = x.UnitPrice,
                    DiscountUnitPrice = x.DiscountUnitPrice,
                    Quantity = x.Quantity,
                }).ToList();
            }

            return Enumerable.Empty<ReceiptDetailDto>();
        }
    }
}
