using Application.Interfaces.ITicketSector;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class TicketSectorCommand : ITicketSectorCommand
    {
        private readonly AppDbContext _context;

        public TicketSectorCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertTicketSectorAsync(TicketSector ticketSector, CancellationToken cancellationToken = default)
        {
            await _context.TicketSectors.AddAsync(ticketSector, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InsertTicketSectorRangeAsync(IEnumerable<TicketSector> ticketSectors, CancellationToken cancellationToken = default)
        {
            await _context.TicketSectors.AddRangeAsync(ticketSectors, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
