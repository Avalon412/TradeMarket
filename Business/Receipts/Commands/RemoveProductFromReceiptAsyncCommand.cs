using MediatR;

namespace Business.Receipts.Commands
{
    public class RemoveProductFromReceiptAsyncCommand : IRequest
    {
        public int ProductId { get; set; }
        public int ReceiptId { get; set; }
        public int Quantity { get; set; }
    }
}
