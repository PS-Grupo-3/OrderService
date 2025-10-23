namespace Application.Models.Requests
{
    public class Details
    {
        public Guid TicketId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
