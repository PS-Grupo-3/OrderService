namespace Application.Models.Responses
{
    public class OrderDetailsResponse
    {
        public Guid DetailId { get; set; }
        public Guid TicketId { get; set; }

        public string? transactionId { get; set; }
        
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
