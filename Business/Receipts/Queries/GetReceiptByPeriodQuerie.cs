using Business.Receipts.DTOS;
using MediatR;

namespace Business.Receipts.Queries
{
    public class GetReceiptByPeriodQuerie : IRequest<IEnumerable<ReceiptReadDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
