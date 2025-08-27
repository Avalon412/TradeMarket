namespace Business.Products.DTOs
{
    public record FilterSearchDto
    {
        public int? CategoryId { get; init; }
        public int? MinPrice { get; init; }
        public int? MaxPrice { get; init; }

        public bool IsEmpty() => CategoryId == null && MinPrice == null && MaxPrice == null;
    }
}
