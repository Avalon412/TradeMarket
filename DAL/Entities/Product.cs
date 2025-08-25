using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Product : EntityBase
    {
        public int ProductCategoryId { get; set; }
        [StringLength(50)]
        public string? ProductName { get; set; }
        public decimal Price { get; set; }

        public ProductCategory Category { get; set; } = null!;
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = null!;
    }
}
