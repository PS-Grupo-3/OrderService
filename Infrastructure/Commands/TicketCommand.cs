using Application.Interfaces.ITicket;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class TicketCommand : ITicketCommand
    {
        private readonly AppDbContext _context;

        public TicketCommand(AppDbContext context)
        {
            _context = context;
        }


        public async Task InsertTicketAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTicketAsync(Ticket ticket, CancellationToken cancellation = default)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
