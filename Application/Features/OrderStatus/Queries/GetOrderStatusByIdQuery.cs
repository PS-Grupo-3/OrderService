using Application.Models.Responses;
using MediatR;

namespace Application.Features.OrderStatus.Queries
{
    public record GetOrderStatusByIdQuery(int orderStatusId) : IRequest<OrderStatusResponse>;
}
