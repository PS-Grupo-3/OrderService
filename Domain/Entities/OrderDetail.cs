namespace Domain.Entities
{
    public class OrderDetail
    {
        public Guid DetailId { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public bool IsSeat { get; set; }
        public Guid EventId { get; set; }
        public Guid? EventSeatId { get; set; }
        public Guid EventSectorId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
    }
}
