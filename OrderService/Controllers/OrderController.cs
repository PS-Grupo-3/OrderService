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
            try
            {
                var result = await _mediator.Send(new GetAllOrdersQuery(from, to, status));
                return Ok(result);
            }
            catch (ArgumentException exception) 
            {
                return BadRequest(new ApiError { message = exception.Message });
            }
            
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var result = await _mediator.Send(new GetOrderByIdQuery(Id));
                return Ok(result);

            }
            catch (NotFoundException404 exception) 
            {
                return NotFound(new ApiError { message = exception.Message });
            }


        }

        [HttpPatch]
      
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderPaymentStatusRequest request)
        {
            try
            {
                var result = await _mediator.Send(new UpdateOrderPaymentStatusCommand(request));
                return Ok(result);

            }
            catch (NotFoundException404 exception)
            {
                return NotFound(new ApiError { message = exception.Message });
            }
            catch (BadRequestException400 exception)
            {
                return BadRequest(new ApiError { message = exception.Message });
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new ApiError { message = exception.Message });
            }
        }
    }
}
