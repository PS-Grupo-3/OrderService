namespace Application.Models.Responses
{
    public class CompleteOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public double TotalAmount { get; set; }
        public GenericResponse PaymentType { get; set; }
        public GenericResponse PaymentStatus { get; set; }
        public GenericResponse OrderStatus { get; set; }
        public List<OrderDetailsResponse> Details { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
