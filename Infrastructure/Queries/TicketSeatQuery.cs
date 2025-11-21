using Application.Interfaces.ITicketSeat;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class TicketSeatQuery : ITicketSeatQuery
    {
        private readonly AppDbContext _context;

        public TicketSeatQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketSeat>> GetSeatsByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            return await _context.TicketSeats
                .Where(ts => ts.TicketId == ticketId)
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketSeat?> GetSeatByEventSeatIdAsync(Guid eventSeatId, CancellationToken cancellationToken = default)
        {
            return await _context.TicketSeats
                .FirstOrDefaultAsync(ts => ts.EventSeatId == eventSeatId, cancellationToken);
        }
    }
}
