namespace Business.Products.DTOs
{
    public record ProductReadDto
    {
        public int Id { get; init; }
        public int ProductCategoryId { get; init; }
        public string? CategoryName { get; init; }
        public string? ProductName { get; init; }
        public decimal Price { get; init; }
        public required ICollection<int> ReceiptDetailIds { get; init; }
    }
}
