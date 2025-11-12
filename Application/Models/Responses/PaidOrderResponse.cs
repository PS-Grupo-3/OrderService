namespace Application.Models.Responses
{
    public class PaidOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid Event { get; set; }
        public Guid Venue { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Transaction { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
