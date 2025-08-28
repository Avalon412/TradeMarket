namespace Business.Receipts.DTOS
{
    public record ReceiptWriteDto
    {
        public int Id { get; init; }
        public int CustomerId { get; init; }
        public DateTime OperationDate { get; init; }
        public bool IsCheckedOut { get; init; }
    }
}
