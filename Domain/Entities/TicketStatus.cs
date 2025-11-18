namespace Domain.Entities
{
    public class TicketStatus
    {
        public int StatusID { get; set; }
        public string Name { get; set; }

        // Relashionsships
        public ICollection<Ticket> Tickets { get; set; }
    }
}
