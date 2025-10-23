using Application.Exceptions;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Order.Queries
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, CompleteOrderResponse>
    {
        private readonly IOrderQuery _query;

        public GetOrderByIdHandler(IOrderQuery query)
        {
            this._query = query;
        }

        public async Task<CompleteOrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await  _query.GetByIdAsync(request.orderId);
            if (order == null) 
            {
            throw new NotFoundException404($"No se encontraron ordenes con el id {request.orderId}");
            }

            return new CompleteOrderResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                PaymentType = new PaymentResponse
                {
                    Id = order.PaymentType.PaymentId,
                    PaymentName = order.PaymentType.PaymentName
                },
                PaymentStatus = new PaymentResponse
                {
                    Id = order.PaymentStatus.PaymentStatusId,
                    PaymentName = order.PaymentStatus.PaymentStatusName
                },
                OrderStatus = new OrderStatusResponse
                {
                    Id = order.OrderStatus.OrderStatusId,
                    StatusName = order.OrderStatus.StatusName
                },
                Details = order.OrderDetails.Select(od => new OrderDetailsResponse
                {
                    DetailId = od.DetailId,
                    TicketId = od.TicketId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    SubTotal = od.Subtotal
                }).ToList(),
                CreateAt = order.BuyDate,
            };
            
        }
    }
}
