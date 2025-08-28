using DAL.Interfaces;
using MediatR;

namespace Business.Receipts.Commands.Handlers
{
    public class RemoveProductFromReceiptAsyncHandler : IRequestHandler<RemoveProductFromReceiptAsyncCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductFromReceiptAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveProductFromReceiptAsyncCommand request, CancellationToken cancellationToken)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(request.ReceiptId);

            var receiptDetail = receipt!.ReceiptDetails.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (receiptDetail is not null)
            {
                receiptDetail.Quantity -= request.Quantity;
                if (receiptDetail.Quantity <= 0)
                    _unitOfWork.ReceiptDetailRepository.Delete(receiptDetail);
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
