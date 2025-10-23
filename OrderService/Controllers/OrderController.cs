using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Order.Queries;
using Application.Features.Order.Commands;
using Application.Models.Requests;


namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        public OrderController(IMediator _mediator) 
        {
        mediator= _mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await mediator.Send(new CreateOrder(request));
            return Ok(result);
        
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetOrderById(Guid Id)
        {

            var result = await mediator.Send(new GetOrderByIdQuery(Id));
            return Ok(result); 
        
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderPaymentStatusRequest request)
        {
            
            var result = await mediator.Send(new UpdateOrderPaymentStatus(request));
            return Ok(result);
        }
    }
}
