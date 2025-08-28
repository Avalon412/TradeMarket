using Business.Receipts.DTOS;
using DAL.Interfaces;
using MediatR;

namespace Business.Receipts.Queries.Handlers
{
    public class GetReceiptByPeriodHandler : IRequestHandler<GetReceiptByPeriodQuerie, IEnumerable<ReceiptReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptByPeriodHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReceiptReadDto>> Handle(GetReceiptByPeriodQuerie request, CancellationToken cancellationToken)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            var filteredReceipts = receipts.Where(x => x.OperationDate >= request.StartDate && x.OperationDate <= request.EndDate);

            var receiptDtos = new List<ReceiptReadDto>();

            foreach (var receipt in filteredReceipts)
            {
                var receiptDto = new ReceiptReadDto
                {
                    Id = receipt.Id,
                    CustomerId = receipt.CustomerId,
                    IsCheckedOut = receipt.IsCheckedOut,
                    OperationDate = receipt.OperationDate,
                    ReceiptDetailsIds = receipt.ReceiptDetails.Select(x => x.Id).ToList(),
                };

                receiptDtos.Add(receiptDto);
            }

            return receiptDtos;
        }
    }
}
