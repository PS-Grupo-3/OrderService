namespace Application.Models.Responses
{
    public class CompleteOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId {  get; set; }
        public Guid VenueId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency {  get; set; }
        public GenericResponse PaymentType { get; set; }
        public GenericResponse OrderStatus { get; set; }
        public List<OrderDetailsResponse> Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Transaction {  get; set; }
    }
}
