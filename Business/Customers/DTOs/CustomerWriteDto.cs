namespace Business.Customers.DTOs
{
    public record CustomerWriteDto
    {
        public string? Name { get; init; }
        public string? Surname { get; init; }
        public DateTime BirthDate { get; init; }
        public int DiscountValue { get; init; }
    }
}
