
namespace Application.Models.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public double TotalAmount { get; set; }
        public PaymentResponse Payment { get; set; }
        public PaymentStatusResponse PaymentStatus { get; set; }
        public OrderStatusResponse OrderStatus { get; set; }

    }
}
