using Application.Interfaces.Adapter;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class ExpiredOrderHandler : IRequestHandler<ExpiredOrderCommand, Unit>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IEventServiceClient _eventServiceClient;

        public ExpiredOrderHandler(
            IOrderCommand command,
            IOrderQuery query,
            IEventServiceClient eventServiceClient)
        {
            _command = command;
            _query = query;
            _eventServiceClient = eventServiceClient;
        }

        public async Task<Unit> Handle(ExpiredOrderCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("[ExpiredOrderHandler] Buscando órdenes expiradas...");
            var expiredOrders = await _query.GetExpiredOrders(cancellationToken);
            Console.WriteLine($"[ExpiredOrderHandler] Encontradas: {expiredOrders.Count()}");

            foreach (var order in expiredOrders)
            {
                Console.WriteLine($"[ExpiredOrderHandler] Procesando orden {order.OrderId}");

                try
                {
                    var seats = order.OrderDetails
                        .Where(d => d.IsSeat && d.EventSeatId.HasValue)
                        .Select(d => d.EventSeatId!.Value)
                        .ToList();

                    if (seats.Any())
                    {
                        Console.WriteLine($"Restaurando {seats.Count} seats");
                        await _eventServiceClient.MarkSeatsAsAvailableAsync(order.EventId, seats, cancellationToken);
                    }

                    var sectors = order.OrderDetails
                        .Where(d => !d.IsSeat)
                        .Select(d => d.EventSectorId)
                        .Distinct()
                        .ToList();

                    foreach (var sectorId in sectors)
                    {
                        Console.WriteLine("Restaurando sector " + sectorId);
                        await _eventServiceClient.MarkSectorAsAvailableAsync(sectorId, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error restaurando seats/sectors para orden " + order.OrderId + ": " + ex.Message);
                }
            }

            try
            {
                Console.WriteLine("Eliminando órdenes...");
                await _command.DeleteRangeAsync(expiredOrders, cancellationToken);
                Console.WriteLine("Eliminadas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error eliminando órdenes expiradas: " + ex.Message);
            }

            return Unit.Value;
        }
    }
}
