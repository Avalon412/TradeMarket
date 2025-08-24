namespace DAL.Entities
{
    public class ReceiptDetail : EntityBase
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountUnitPrice { get; set; }
        public int Quantity { get; set; }

        public Receipt Receipt { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
