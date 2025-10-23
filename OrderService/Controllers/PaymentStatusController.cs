using Application.Features.PaymentStatus.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPaymentStatusesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPaymentStatusByIdQuery(id));
            return Ok(result);
        }
    }
}
