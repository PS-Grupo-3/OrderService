using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketSeat
    {
        public Guid TicketSeatId { get; set; }
        public Guid TicketId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventSectorId { get; set; }
        public Guid EventSeatId { get; set; }
        public decimal Price { get; set; }

        public Ticket TicketRef { get; set; }
    }
}
