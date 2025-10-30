using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Commands
{
    public record DeleteOrderCommand(Guid orderId):IRequest<OrderResponse>;
}
