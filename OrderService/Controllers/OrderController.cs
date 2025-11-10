using Application.Features.Order.Commands;
using Application.Features.Order.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles="Current")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId}, result);
        }

        [HttpGet]
        [Authorize (Roles ="Admin,SuperAdmin")]
        public async Task<IActionResult> GetAll(DateTime? from, DateTime? to, int? paymentType, Guid? userId)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(from, to, paymentType, userId));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(Id));
            return Ok(result);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Current")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateOrderRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderCommand(Id, request));
            return Ok(result);
        }

       


    }
}
