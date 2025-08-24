namespace DAL.Entities
{
    public class Receipt : EntityBase
    {
        public int CustomerId { get; set; }
        public DateTime OperationDate { get; set; }
        public bool IsCheckedOut { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = null!;
    }
}
