using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicketSeat
{
    public interface ITicketSeatQuery
    {
        Task<IEnumerable<TicketSeat>> GetSeatsByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
        Task<TicketSeat?> GetSeatByEventSeatIdAsync(Guid eventSeatId, CancellationToken cancellationToken = default);
    }
}
