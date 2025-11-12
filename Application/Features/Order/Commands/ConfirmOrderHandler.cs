using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Constants;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand, PaidOrderResponse>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IPaymentTypeQuery _queryPayment;

        public ConfirmOrderHandler(IOrderCommand command, IOrderQuery query, IPaymentTypeQuery queryPayment)
        {
            _command = command;
            _query = query;
            _queryPayment = queryPayment;
        }

        public async Task<PaidOrderResponse> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
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

            if (order.OrderStatus.OrderStatusName == OrderStatusNames.Paid)
            {
                throw new ConflictException($"La orden con el ID {request.Id} ya fue pagada.");
            }

            if (request.request.PaymentType <= 0)
            {
                throw new ArgumentException($"El método de pago con el ID {request.request.PaymentType} es inválido");
            }

            var paymentType = await _queryPayment.GetByIdAsync(request.request.PaymentType);

            if (paymentType is null)
            {
                throw new KeyNotFoundException($"No se encontró método de pago con el ID {request.request.PaymentType}.");
            }

            foreach (var detail in order.OrderDetails)
            {
                detail.TicketId = Guid.NewGuid();
                detail.UpdatedAt = DateTime.UtcNow;
            }

            order.OrderStatusId = OrderStatusIds.Paid;
            order.PaymentId = request.request.PaymentType;
            order.PaymentDate = DateTime.UtcNow;
            order.Currency = request.request.Currency;
            order.UpdatedAt = DateTime.UtcNow;

            // Generar código de transacción según PaymentTypeNames
            string transactionPrefix = paymentType.PaymentName switch
            {
                PaymentTypeNames.MercadoPago => "MP-",
                PaymentTypeNames.Cash => "EF-",
                PaymentTypeNames.MasterCard => "MC-",
                PaymentTypeNames.Visa => "VI-",
                PaymentTypeNames.PayPal => "PP-",
                _ => "OT-" // Otros métodos
            };

            order.TransactionId = $"{transactionPrefix}{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12).ToUpper()}";

            await _command.UpdateAsync(order, cancellationToken);

            return new PaidOrderResponse
            {
                OrderId = order.OrderId,
                Event = order.EventId,
                Venue = order.VenueId,
                PaymentDate = order.PaymentDate.Value,
                Transaction = order.TransactionId,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
