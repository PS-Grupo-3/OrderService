

using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Queries
{
    public record GetOrderByIdQuery(Guid orderId):IRequest<OrderResponse>;    
}
