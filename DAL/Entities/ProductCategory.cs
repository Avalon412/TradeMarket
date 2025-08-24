namespace DAL.Entities
{
    public class ProductCategory : EntityBase
    {
        public string? CategoryName { get; set; }

        public ICollection<Product> Products { get; set; } = null!;
    }
}
