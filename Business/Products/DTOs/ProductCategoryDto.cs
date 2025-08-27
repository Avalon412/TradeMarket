namespace Business.Products.DTOs
{
    public record ProductCategoryDto
    {
        public int Id { get; init; }
        public string? CategoryName { get; init; }
    }
}
