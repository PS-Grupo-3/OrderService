using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class OrderDetailsResponse
    {
        public Guid DetailId { get; set; }
        public Guid TicketId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
