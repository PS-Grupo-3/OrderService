namespace Application.Models.Responses
{
    public class CompleteOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Event {  get; set; }
        public DateTime EvemtDate { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public string Currency {  get; set; }
        public GenericResponse PaymentType { get; set; }
        public GenericResponse PaymentStatus { get; set; }
        public List<OrderDetailsResponse> Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Transaction {  get; set; }
    }
}
