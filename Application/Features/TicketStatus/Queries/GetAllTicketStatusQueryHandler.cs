using Application.Interfaces.ITicketStatus;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketStatus.Queries
{
    public class GetAllTicketStatusQueryHandler : IRequestHandler<GetAllTicketStatusQuery, List<TicketStatusResponse>>
    {
        private readonly ITicketStatusQuery _ticketStatusQuery;

        public GetAllTicketStatusQueryHandler(ITicketStatusQuery ticketStatusQuery)
        {
            _ticketStatusQuery = ticketStatusQuery;
        }

        public async Task<List<TicketStatusResponse>> Handle(GetAllTicketStatusQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _ticketStatusQuery.GetAllTicketStatusAsync();
            return statuses.Select(status => new TicketStatusResponse
            {
                StatusID = status.StatusID,
                Name = status.Name,
            }).ToList();
        }
    }
}
