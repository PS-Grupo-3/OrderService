namespace Domain.Entities
{
    public class OrderDetail
    {
        public Guid DetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid TicketId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }

        public Order Order { get; set; }
    }
}
