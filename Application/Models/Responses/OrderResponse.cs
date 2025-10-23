
namespace Application.Models.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public DateTime CreateAt { get; set; }
        public double TotalAmount { get; set; }
    }
}
