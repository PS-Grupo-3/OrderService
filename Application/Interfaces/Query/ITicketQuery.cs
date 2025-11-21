using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicket
{
    public interface ITicketQuery
    {
        Task<Ticket?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Ticket>> GetTicketsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Ticket>> GetTicketsByEventAndUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken = default);
    }
}
