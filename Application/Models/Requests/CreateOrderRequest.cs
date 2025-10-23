namespace Application.Models.Requests
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public int PaymentId { get; set; }
    }
}
