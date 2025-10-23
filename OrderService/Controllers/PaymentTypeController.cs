using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.PaymentType.Queries;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentTypeController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPaymentTypesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPaymentTypeByIdQuery(id));
            return Ok(result);
        }
    }
}
