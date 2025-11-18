namespace Domain.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid OrderDetailId { get; set; }
        public int StatusId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        // Relationships
        public TicketStatus StatusRef { get; set; }
    }
}
