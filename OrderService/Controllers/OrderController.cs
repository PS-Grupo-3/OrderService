using Application.Exceptions;
using Application.Features.Order.Commands;
using Application.Features.Order.Queries;

using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
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
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }

        [HttpGet]
     
        public async Task<IActionResult> GetAll(DateTime? from, DateTime? to, int? status)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(from, to, status));
            return Ok(result);
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(Id));
            return Ok(result);
        }

        [HttpPut("{Id:Guid}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] DetailsUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderDetailsCommand(Id, request));
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrderStatus(Guid Id, [FromBody] UpdateStatusRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderPaymentStatusCommand(Id, request));
            return Ok(result);
        }
    }
}
