using Domain.Entities;


namespace Application.Interfaces.ITicket
{
    public interface ITicketCommand
    {
        Task InsertTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task UpdateTicketAsync(Ticket ticket, CancellationToken cancellation = default);
    }
}
