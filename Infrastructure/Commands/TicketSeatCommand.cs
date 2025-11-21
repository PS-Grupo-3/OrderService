using Application.Interfaces.ITicketSeat;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class TicketSeatCommand : ITicketSeatCommand
    {
        private readonly AppDbContext _context;

        public TicketSeatCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertTicketSeatAsync(TicketSeat ticketSeat, CancellationToken cancellationToken = default)
        {
            await _context.TicketSeats.AddAsync(ticketSeat, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InsertTicketSeatRangeAsync(IEnumerable<TicketSeat> ticketSeats, CancellationToken cancellationToken = default)
        {
            await _context.TicketSeats.AddRangeAsync(ticketSeats, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
