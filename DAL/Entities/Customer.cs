using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Customer : EntityBase
    {
        public int PersonId { get; set; }
        public int DiscountValue { get; set; }

        public Person Person { get; set; } = null!;
        public ICollection<Receipt> Receipts { get; set; } = null!;
    }
}
