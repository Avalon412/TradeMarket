namespace Business.Products.DTOs
{
    public record ProductWriteDto
    {
        public int ProductId { get; init; }
        public int ProductCategoryId { get; init; }
        public string? ProductName { get; init; }
        public decimal Price { get; init; }
    }
}
