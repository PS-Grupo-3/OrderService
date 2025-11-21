using Application.Exceptions;
using Application.Interfaces.Adapter;
using Application.Interfaces.Command;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketSeat;
using Application.Interfaces.ITicketSector;
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
        private readonly ITicketCommand _ticketCommand;
        private readonly ITicketSeatCommand _ticketSeatCommand;
        private readonly ITicketSectorCommand _ticketSectorCommand;
        private readonly IEventServiceClient _eventServiceClient;

        public ConfirmOrderHandler(IOrderCommand command, IOrderQuery query, IPaymentTypeQuery queryPayment, ITicketCommand ticketCommand,
            ITicketSeatCommand ticketSeatCommand, ITicketSectorCommand ticketSectorCommand, IEventServiceClient eventServiceClient)
        {
            _command = command;
            _query = query;
            _queryPayment = queryPayment;
            _ticketCommand = ticketCommand;
            _ticketSeatCommand = ticketSeatCommand;
            _ticketSectorCommand = ticketSectorCommand;
            _eventServiceClient = eventServiceClient;
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

            if (order.OrderDetails.Count() < 1)
            {
                throw new ArgumentException($"La orden con el ID {request.Id} debe tener al menos un detalle.");
            }

            // Crear el ticket
            var ticketId = Guid.NewGuid();

            var ticket = new Domain.Entities.Ticket
            {
                TicketId = ticketId,
                OrderId = order.OrderId,
                EventId = order.EventId,
                UserId = order.UserId,
                StatusId = 1, // inicia como activo
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };

            await _ticketCommand.InsertTicketAsync(ticket);

            // se procesa cada detalle de la orden para crear ticketSeat o ticketSector
            var seatsToLock = new List<Guid>();

            foreach (var detail in order.OrderDetails)
            {
                if (detail.IsSeat) // si es un asiento
                {
                    var seat = new Domain.Entities.TicketSeat
                    {
                        TicketSeatId = Guid.NewGuid(),
                        TicketId = ticketId,
                        EventId = order.EventId,
                        EventSectorId = detail.EventSectorId,
                        EventSeatId = detail.EventSeatId.Value,
                        Price = detail.UnitPrice
                    };
                    await _ticketSeatCommand.InsertTicketSeatAsync(seat);
                    seatsToLock.Add(detail.EventSeatId.Value);
                }
                else // si es un sector no controlado
                {
                    var sector = new Domain.Entities.TicketSector
                    {
                        TicketSectorId = Guid.NewGuid(),
                        TicketId = ticketId,
                        EventId = order.EventId,
                        EventSectorId = detail.EventSectorId,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice
                    };
                    await _ticketSectorCommand.InsertTicketSectorAsync(sector);
                }
            }
            // notificar a EventService
            if (seatsToLock.Any())
            {
                await _eventServiceClient.MarkSeatsAsUnavailableAsync(order.EventId, seatsToLock, cancellationToken);
            }

            // actualzia la orden
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
