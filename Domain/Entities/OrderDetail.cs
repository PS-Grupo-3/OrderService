namespace Domain.Entities
{
    public class OrderDetail
    {
        public Guid DetailId { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid TicketId { get; set; }
        public string SectorName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Subtotal { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }

        public Order Order { get; set; }
    }
}
