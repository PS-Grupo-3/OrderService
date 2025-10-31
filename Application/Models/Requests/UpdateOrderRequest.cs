namespace Application.Models.Requests
{
    public class UpdateOrderRequest
    {
        public int PaymentType { get; set; }
        public string Currency { get; set; }
        public List<Details> Details { get; set; }
    }
}