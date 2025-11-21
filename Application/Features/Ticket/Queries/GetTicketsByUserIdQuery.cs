using Application.Models.Responses;
using MediatR;

namespace Application.Features.Ticket.Queries
{
    public record GetTicketsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<TicketResponse>>;
}
