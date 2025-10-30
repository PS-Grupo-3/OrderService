﻿using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Queries
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, CompleteOrderResponse>
    {
        private readonly IOrderQuery _query;

        public GetOrderByIdHandler(IOrderQuery query)
        {
            _query = query;
        }

        public async Task<CompleteOrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await  _query.GetByIdAsync(request.orderId);
            if (order == null) 
            {
                throw new KeyNotFoundException($"No se encontraron ordenes con el ID {request.orderId}");
            }

            return new CompleteOrderResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                PaymentType = new GenericResponse
                {
                    Id = order.PaymentType.PaymentId,
                    Name = order.PaymentType.PaymentName
                },
                PaymentStatus = new GenericResponse
                {
                    Id = order.PaymentStatus.PaymentStatusId,
                    Name = order.PaymentStatus.PaymentStatusName
                },
                OrderStatus = new GenericResponse
                {
                    Id = order.OrderStatus.OrderStatusId,
                    Name = order.OrderStatus.StatusName
                },
                Details = order.OrderDetails.Select(od => new OrderDetailsResponse
                {
                    DetailId = od.DetailId,
                    TicketId = od.TicketId,
                    transactionId=od.transactionId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    SubTotal = od.Subtotal
                }).ToList(),
                CreateAt = order.BuyDate,
            };
            
        }
    }
}
