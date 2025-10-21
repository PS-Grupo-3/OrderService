
using MediatR;
using Application.Features.Payment.Queries;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;
        public PaymentController(IMediator _mediator) 
        {
            mediator = _mediator;
        }






        [HttpGet("test")]
        public async Task<IActionResult> GetAllPayments()
        {
            var result =  await mediator.Send(new GetAllPaymentsQuery());
            return Ok(result);
        }

    }
}
