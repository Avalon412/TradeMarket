namespace Business.Receipts.DTOS
{
    public record ReceiptDetailDto
    {
        public int Id { get; init; }
        public int ReceiptId { get; init; }
        public int ProductId { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal DiscountUnitPrice { get; init; }
        public int Quantity { get; init; }
    }
}
