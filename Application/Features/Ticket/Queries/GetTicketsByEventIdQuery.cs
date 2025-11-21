using Application.Models.Responses;
using MediatR;

namespace Application.Features.Ticket.Queries
{
    public record GetTicketsByEventIdQuery(Guid EventId, Guid? UserId) : IRequest<IEnumerable<TicketResponse>>;
}
