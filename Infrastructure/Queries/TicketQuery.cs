using Application.Interfaces.ITicket;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class TicketQuery : ITicketQuery
    {
        private readonly AppDbContext _context;

        public TicketQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets
                .Include(t => t.TicketSeats)
                .Include(t => t.TicketSectors)
                .Include(t => t.StatusRef)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId, cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets
                .Where(t => t.OrderId == orderId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventAndUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets
                .Where(t => t.EventId == eventId && t.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
