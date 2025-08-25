using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProductCategory : EntityBase
    {
        [StringLength(50)]
        public string? CategoryName { get; set; }

        public ICollection<Product> Products { get; set; } = null!;
    }
}
