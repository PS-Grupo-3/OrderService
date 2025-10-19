namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BuyDate { get; set; }
        public double TotalAmount { get; set; }
        public int PaymentId { get; set; }
        public bool Payment { get; set; }
        public int OrderStatusId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public OrderStatus OrderStatus { get; set; }
    }
}
