using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IPaymentTypeQuery _queryPayment;

        public UpdateOrderHandler(IOrderCommand command, IOrderQuery query, IPaymentTypeQuery queryPayment)
        {
            _command = command;
            _query = query;
            _queryPayment = queryPayment;
        }

        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Currency.Length > 3)
            {
                throw new ArgumentException($"Formato de currency inválido.");
            }
            
            var order = await _query.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"No se encontró la orden con el ID {request.Id}");
            }

            if (order.PaymentStatusId == 2)
            {
                throw new ConflictException($"No se puede modificar una orden ya pagada.");
            }

            if (request.request.PaymentType <= 0)
            {
                throw new ArgumentException($"El método de pago con el ID {request.request.PaymentType} es inválido");
            }

            var PaymentType = await _queryPayment.GetByIdAsync(request.request.PaymentType);

            if (PaymentType is null)
            {
                throw new KeyNotFoundException($"No se encontró método de pago con el ID {request.request.PaymentType}.");
            }
            
            double totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            foreach (var detail in request.request.Details)
            {
                if (detail.UnitPrice <= 0)
                {
                    throw new ArgumentException($"El precio ${detail.UnitPrice} no es válido.");
                }

                if (detail.Quantity <= 0)
                {
                    throw new ArgumentException($"La cantidad {detail.Quantity} no es válido.");
                }

                double baseAmount = detail.UnitPrice * detail.Quantity;
                double discountAmount = baseAmount * ((detail.Discount ?? 0) / 100);
                double taxAmount = baseAmount * ((detail.Tax ?? 0) / 100);

                double subTotal = baseAmount - discountAmount + taxAmount;
                totalAmount += subTotal;

                orderDetails.Add(new OrderDetail
                {
                    DetailId = Guid.NewGuid(),
                    TicketId = detail.TicketId,
                    SectorName = detail.Sector,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    Subtotal = subTotal,
                    Discount = detail.Discount,
                    Tax = detail.Tax
                });
            }

            order.TotalAmount = totalAmount;
            order.Currency = request.request.Currency;
            order.PaymentId = request.request.PaymentType;
            order.PaymentStatusId = 2;
            order.PaymentDate = DateTime.UtcNow;
            order.TransactionId = $"MP-{Guid.NewGuid()}";
            //order.OrderDetails = orderDetails; /* <-- falla en orderdetails. */

            await _command.UpdateAsync(order);

            var updatedOrder = await _query.GetByIdAsync(order.OrderId, cancellationToken);


            return new UpdatedOrderResponse
            {
                Id = updatedOrder.OrderId,
                Event = updatedOrder.EventName,
                EventDate = updatedOrder.EventDate,
                Venue = updatedOrder.VenueName,
                Address = updatedOrder.VenueAddress,
                TotalAmount = updatedOrder.TotalAmount,
                Payment = new GenericResponse
                {
                    Id = updatedOrder.PaymentStatus.PaymentStatusId,
                    Name = updatedOrder.PaymentStatus.PaymentStatusName
                },
                PaymentDate = updatedOrder.PaymentDate,
                Transaction = updatedOrder.TransactionId
            };
        }
    }
}
