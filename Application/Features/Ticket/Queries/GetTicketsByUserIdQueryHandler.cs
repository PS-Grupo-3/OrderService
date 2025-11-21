using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketSeat;
using Application.Interfaces.ITicketSector;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Ticket.Queries
{
    public class GetTicketsByUserIdHandler : IRequestHandler<GetTicketsByUserIdQuery, IEnumerable<TicketResponse>>
    {
        private readonly ITicketQuery _ticketQuery;
        private readonly ITicketSeatQuery _seatQuery;
        private readonly ITicketSectorQuery _sectorQuery;

        public GetTicketsByUserIdHandler(ITicketQuery ticketQuery, ITicketSeatQuery seatQuery, ITicketSectorQuery sectorQuery)
        {
            _ticketQuery = ticketQuery;
            _seatQuery = seatQuery;
            _sectorQuery = sectorQuery;
        }

        public async Task<IEnumerable<TicketResponse>> Handle(GetTicketsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketQuery.GetTicketsByUserIdAsync(request.UserId, cancellationToken);
            var result = new List<TicketResponse>();

            foreach (var ticket in tickets)
            {
                var seats = await _seatQuery.GetSeatsByTicketIdAsync(ticket.TicketId, cancellationToken);
                var sectors = await _sectorQuery.GetSectorsByTicketIdAsync(ticket.TicketId, cancellationToken);

                result.Add(new TicketResponse
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
                });
            }

            return result;
        }
    }
}
