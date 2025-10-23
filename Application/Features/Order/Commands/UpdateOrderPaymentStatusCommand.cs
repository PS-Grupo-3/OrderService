using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;


namespace Application.Features.Order.Commands
{
    public record UpdateOrderPaymentStatusCommand(UpdateOrderPaymentStatusRequest request):IRequest<OrderResponse>;
}
