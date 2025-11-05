namespace Application.Models.Responses
{
    public class CreatedOrderResponse
    {
        public Guid OrderId { get; set; }
        public string Event { get; set; }
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
