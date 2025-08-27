namespace Business.Customers.DTOs
{
    public record CustomerWriteDto
    {
        public int CustomerId { get; init; }
        public int PersonId { get; init; }
        public string? Name { get; init; }
        public string? Surname { get; init; }
        public DateTime BirthDate { get; init; }
        public int DiscountValue { get; init; }
    }
}
