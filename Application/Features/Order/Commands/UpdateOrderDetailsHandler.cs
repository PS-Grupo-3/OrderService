using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderDetailsHandler : IRequestHandler<UpdateOrderDetailsCommand, OrderResponse>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IOrderDetailQuery _queryDetail;

        public UpdateOrderDetailsHandler(IOrderCommand command, IOrderQuery query, IOrderDetailQuery queryDetail)
        {
            _command = command;
            _query = query;
            _queryDetail = queryDetail;
        }

        public async Task<OrderResponse> Handle(UpdateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var order = await _query.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
                throw new NotFoundException404($"No se encontró la orden con el id {request.Id}");

            if (order.OrderStatusId != 1)
                throw new BadRequestException400($"El estado de la orden {request.Id} no es pendiente.");

            foreach (var detail in request.request.Details)
            {
                if (detail.UnitPrice <= 0)
                    throw new ArgumentException($"El precio {detail.UnitPrice} no es válido.");

                if (detail.Quantity <= 0)
                    throw new ArgumentException($"La cantidad {detail.Quantity} no es válida.");

                var existingDetail = order.OrderDetails
                    .FirstOrDefault(d => d.TicketId == detail.TicketId);

                double subTotal = detail.UnitPrice * detail.Quantity;

                if (existingDetail != null)
                {
                    existingDetail.UnitPrice = detail.UnitPrice;
                    existingDetail.Quantity = detail.Quantity;
                    existingDetail.Subtotal = subTotal;
                }
                else
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        DetailId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        TicketId = detail.TicketId,
                        UnitPrice = detail.UnitPrice,
                        Quantity = detail.Quantity,
                        Subtotal = subTotal
                    });
                }
            }

            order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);

            await _command.UpdateAsync(order);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                CreateAt = order.BuyDate,
                TotalAmount = order.TotalAmount
            };
        }

    }
}
