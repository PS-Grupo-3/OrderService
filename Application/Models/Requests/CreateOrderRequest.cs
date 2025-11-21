namespace Application.Models.Requests
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid Event { get; set; }
        public Guid Venue { get; set; }

        public List<TicketSeatRequest>? Seats { get; set; }
        public List<TicketSectorRequest>? Sectors { get; set; }
    }
}
