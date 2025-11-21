using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicketSeat
{
    public interface ITicketSeatCommand
    {
        Task InsertTicketSeatAsync(TicketSeat ticketSeat, CancellationToken cancellationToken = default);
        Task InsertTicketSeatRangeAsync(IEnumerable<TicketSeat> ticketSeats, CancellationToken cancellation = default);
    }
}
