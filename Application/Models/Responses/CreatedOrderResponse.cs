namespace Application.Models.Responses
{
    public class CreatedOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid Event { get; set; }
        public Guid Venue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
