using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Order.Queries
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderQuery _query;

        public GetOrderByIdHandler(IOrderQuery query)
        {
            this._query = query;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await  _query.GetOrderByIdAsync(request.orderId);

            return new OrderResponse {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Payment = new PaymentResponse
                {
                    Id = order.PaymentId,
                    PaymentName = order.PaymentType.PaymentName,
                },
                PaymentStatus=new PaymentStatusResponse 
                {
                statusId=order.PaymentStatusId,
                StatusName=order.PaymentStatus.PaymentStatusName,
                }
            
            };
        }
    }
}
