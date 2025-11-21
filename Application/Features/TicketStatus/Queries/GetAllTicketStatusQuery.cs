using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketStatus.Queries
{
    public record GetAllTicketStatusQuery() : IRequest<List<TicketStatusResponse>>;
}
