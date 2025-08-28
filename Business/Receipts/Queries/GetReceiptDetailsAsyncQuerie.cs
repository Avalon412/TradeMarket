using Business.Receipts.DTOS;
using MediatR;

namespace Business.Receipts.Queries
{
    public class GetReceiptDetailsAsyncQuerie : IRequest<IEnumerable<ReceiptDetailDto>>
    {
        public int ReceiptId { get; set; }
    }
}
