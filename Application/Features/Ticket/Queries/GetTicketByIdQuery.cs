using Application.Models.Responses;
using MediatR;

namespace Application.Features.Ticket.Queries
{
    public record GetTicketByIdQuery(Guid TicketId) : IRequest<TicketResponse>;
}
