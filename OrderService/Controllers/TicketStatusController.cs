using Application.Features.TicketStatus.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TicketService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var result = await _mediator.Send(new GetAllTicketStatusQuery());
            return Ok(result);
        }
    }
}
