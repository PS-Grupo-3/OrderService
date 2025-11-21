using Application.Features.Ticket.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ticketId:guid}")]
        [Authorize(Roles = "Current,Admin,SuperAdmin")]
        public async Task<IActionResult> GetById(Guid ticketId)
        {
            var result = await _mediator.Send(new GetTicketByIdQuery(ticketId));
            return Ok(result);
        }

        [HttpGet("user/{userId:guid}")]
        [Authorize(Roles = "Current,Admin,SuperAdmin")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var result = await _mediator.Send(new GetTicketsByUserIdQuery(userId));
            return Ok(result);
        }

        [HttpGet("event/{eventId:guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetByEvent(Guid eventId, [FromQuery] Guid? userId)
        {
            var result = await _mediator.Send(new GetTicketsByEventIdQuery(eventId, userId));
            return Ok(result);
        }
    }
}
