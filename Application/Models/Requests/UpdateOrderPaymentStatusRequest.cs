
namespace Application.Models.Requests
{
    public class UpdateOrderPaymentStatusRequest
    {
        public Guid OrderId { get; set; }
        public int PaymentStatusId { get; set; }
    }
}
