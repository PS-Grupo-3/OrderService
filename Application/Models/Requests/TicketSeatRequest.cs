using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class TicketSeatRequest
    {
        public Guid EventSeatId { get; set; }
        public Guid EventSectorId { get; set; }
        public Guid EventId { get; set; }
        public decimal Price { get; set; }
    }
}
