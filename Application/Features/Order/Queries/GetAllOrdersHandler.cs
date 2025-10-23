using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Order.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<CompleteOrderResponse>>
    {
        private readonly IOrderQuery _query;

        public GetAllOrdersHandler(IOrderQuery query)
        {
            this._query = query;
        }
        public async Task<List<CompleteOrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request.from.HasValue && request.to.HasValue && request.from.Value > request.to.Value)
            {
                throw new ArgumentException("Rango de fechas inválidas");
            }

            var orders = await _query.GetAllAsync(request.from, request.to, request.status);

            var response = orders.Select(order => new CompleteOrderResponse
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
            }).ToList();

            return response;
        }
    }
}
