using Application.Interfaces.ITicketSector;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class TicketSectorQuery : ITicketSectorQuery
    {
        private readonly AppDbContext _context;

        public TicketSectorQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketSector>> GetSectorsByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            return await _context.TicketSectors
                .Where(ts => ts.TicketId == ticketId)
                .ToListAsync(cancellationToken);
        }
    }
}
