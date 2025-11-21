using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketSeat;
using Application.Interfaces.ITicketSector;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Ticket.Queries
{
    public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, TicketResponse>
    {
        private readonly ITicketQuery _ticketQuery;
        private readonly ITicketSeatQuery _seatQuery;
        private readonly ITicketSectorQuery _sectorQuery;

        public GetTicketByIdHandler(ITicketQuery ticketQuery, ITicketSeatQuery seatQuery, ITicketSectorQuery sectorQuery)
        {
            _ticketQuery = ticketQuery;
            _seatQuery = seatQuery;
            _sectorQuery = sectorQuery;
        }

        public async Task<TicketResponse> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketQuery.GetTicketByIdAsync(request.TicketId, cancellationToken);

            if (ticket == null)
                throw new KeyNotFoundException($"No se encontró el ticket {request.TicketId}");

            var seats = await _seatQuery.GetSeatsByTicketIdAsync(ticket.TicketId, cancellationToken);
            var sectors = await _sectorQuery.GetSectorsByTicketIdAsync(ticket.TicketId, cancellationToken);

            return new TicketResponse
            {
                TicketId = ticket.TicketId,
                OrderId = ticket.OrderId,
                UserId = ticket.UserId,
                EventId = ticket.EventId,
                StatusId = ticket.StatusId,
                Created = ticket.Created,
                Updated = ticket.Updated,

                Seats = seats.Select(s => new TicketSeatResponse
                {
                    TicketSeatId = s.TicketSeatId,
                    EventId = s.EventId,
                    EventSectorId = s.EventSectorId,
                    EventSeatId = s.EventSeatId,
                    Price = s.Price
                }).ToList(),

                Sectors = sectors.Select(sec => new TicketSectorResponse
                {
                    TicketSectorId = sec.TicketSectorId,
                    EventId = sec.EventId,
                    EventSectorId = sec.EventSectorId,
                    Quantity = sec.Quantity,
                    UnitPrice = sec.UnitPrice
                }).ToList()
            };
        }
    }
}
