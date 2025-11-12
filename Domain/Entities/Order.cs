namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        public Guid EventId { get; set; }
        public Guid VenueId { get; set; }

        public decimal TotalAmount { get; set; }
        public string Currency {  get; set; }

        public int? PaymentId { get; set; }
        public int OrderStatusId { get; set; }
        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        
        public PaymentType? PaymentType { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
