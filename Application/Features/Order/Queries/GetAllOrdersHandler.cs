using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<CompleteOrderResponse>>
    {
        private readonly IOrderQuery _query;

        public GetAllOrdersHandler(IOrderQuery query)
        {
            _query = query;
        }
        public async Task<List<CompleteOrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request.from.HasValue && request.to.HasValue && request.from.Value > request.to.Value)
            {
                throw new ArgumentException($"Rango de fechas inválidos");
            }
            if (request.status <=0 || request.status > 3) 
            {
                throw new ArgumentException($"El estado de la órden con el ID {request.status} es inválido.");
            }

            var orders = await _query.GetAllAsync(request.from, request.to, request.status, request.userId);

            var response = orders.Select(order => new CompleteOrderResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                EventId = order.EventId,
                VenueId = order.VenueId,
                Currency = order.Currency,
                TotalAmount = order.TotalAmount,
                PaymentType = new GenericResponse
                {
                    Id = order.PaymentType.PaymentId,
                    Name = order.PaymentType.PaymentName
                },
                OrderStatus = new GenericResponse
                {
                    Id = order.OrderStatus.OrderStatusId,
                    Name = order.OrderStatus.OrderStatusName
                },
                Details = order.OrderDetails.Select(od => new OrderDetailsResponse
                {
                    DetailId = od.DetailId,
                    TicketId = od.TicketId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    SubTotal = od.Subtotal,
                    DiscountAmount = od.DiscountAmount,
                    TaxAmount = od.TaxAmount
                }).ToList(),
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                PaymentDate = order.PaymentDate,
                Transaction = order.TransactionId
            }).ToList();

            return response;
        }
    }
}
