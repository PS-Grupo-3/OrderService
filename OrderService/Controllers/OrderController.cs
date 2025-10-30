using Application.Features.Order.Commands;
using Application.Features.Order.Queries;
using Application.Models.Requests;
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
        public async Task<IActionResult> GetAll(DateTime? from, DateTime? to, int? status, Guid? userId)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(from, to, status, userId));
            return Ok(result);
        }

        [HttpGet("{Id}")]
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

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid Id, [FromBody] UpdateStatusRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderPaymentStatusCommand(Id, request));
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(Guid Id) 
        {
            var result = await _mediator.Send(new DeleteOrderCommand(Id));
            return Ok(result);
        }


    }
}
