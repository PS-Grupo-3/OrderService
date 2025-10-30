namespace Domain.Entities
{
    public class OrderDetail
    {
        public Guid DetailId { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid TicketId { get; set; }
        public string? transactionId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }

        public Order Order { get; set; }
    }
}
