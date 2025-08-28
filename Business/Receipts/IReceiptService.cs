using Business.Receipts.DTOS;
using Business.Shared;

namespace Business.Receipts
{
    public interface IReceiptService : ICrud<ReceiptReadDto, ReceiptWriteDto>
    {
        Task<decimal> ToPayAsync(int receiptId);
        Task CheckOutAsync(int receiptId);
    }
}
