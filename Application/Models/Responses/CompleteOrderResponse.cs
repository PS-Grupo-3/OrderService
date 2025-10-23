namespace Application.Models.Responses
{
    public class CompleteOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public double TotalAmount { get; set; }
        public PaymentResponse PaymentType { get; set; }
        public PaymentResponse PaymentStatus { get; set; }
        public OrderStatusResponse OrderStatus { get; set; }
        public List<OrderDetailsResponse> Details { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
