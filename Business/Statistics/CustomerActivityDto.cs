namespace Business.Statistics
{
    public record CustomerActivityDto
    {
        public int CustomerId { get; init; }
        public string? CustomerName { get; init; }
        public decimal ReceiptSum { get; init; }
    }
}
