namespace Application.Models.Requests
{
    public class PaidOrderRequest
    {
        public string Currency { get; set; }
        public int PaymentType { get; set; }
    }
}
