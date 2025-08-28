using DAL.Entities;
using DAL.Interfaces;
using MediatR;

namespace Business.Receipts.Commands.Handlers
{
    public class AddProductToReceiptAsyncHandler : IRequestHandler<AddProductToReceiptAsyncCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProductToReceiptAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddProductToReceiptAsyncCommand request, CancellationToken cancellationToken)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(request.ReceiptId);

            var receiptDetail = receipt!.ReceiptDetails.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (receiptDetail is not null)
            {
                receiptDetail.Quantity += request.Quantity;
            }
            else
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
                if (product is not null)
                {
                    var discountValue = receipt.Customer.DiscountValue;

                    var newReceiptDetail = new ReceiptDetail
                    {
                        Product = product,
                        ProductId = product.Id,
                        Receipt = receipt,
                        ReceiptId = receipt.Id,
                        Quantity = request.Quantity,
                        UnitPrice = product.Price,
                        DiscountUnitPrice = product.Price * (1 - ((decimal)discountValue / 100))
                    };

                    _unitOfWork.ReceiptDetailRepository.Add(newReceiptDetail);
                }
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
