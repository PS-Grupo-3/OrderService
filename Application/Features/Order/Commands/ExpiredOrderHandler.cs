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
                Console.WriteLine($"Procesando orden {order.OrderId}");

                try
                {
                    var controlledSeatIds = order.OrderDetails
                        .Where(d => d.IsSeat && d.EventSeatId.HasValue)
                        .Select(d => d.EventSeatId!.Value)
                        .ToList();

                    if (controlledSeatIds.Any())
                    {
                        Console.WriteLine($"Liberando {controlledSeatIds.Count} seats controlados...");
                        await _eventServiceClient.MarkSeatsAsAvailableAsync(
                            order.EventId, controlledSeatIds, cancellationToken);
                    }
                    
                    var sectorDetails = order.OrderDetails
                        .Where(d => !d.IsSeat)
                        .ToList();

                    foreach (var detail in sectorDetails)
                    {
                        var sectorId = detail.EventSectorId;

                        var sectorInfo = await _eventServiceClient.GetSectorAsync(
                            order.EventId, sectorId, cancellationToken);

                        if (sectorInfo.IsControlled)
                        {
                            Console.WriteLine($"Sector controlado {sectorId}: liberando SECTOR entero");
                            await _eventServiceClient.MarkSectorAsAvailableAsync(sectorId, cancellationToken);
                        }
                        else
                        {
                            Console.WriteLine($"Sector NO controlado {sectorId}: liberando {detail.Quantity} unidades...");

                            for (int i = 0; i < detail.Quantity; i++)
                            {
                                await _eventServiceClient.ReleaseFreeSectorAsync(sectorId, cancellationToken);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error liberando recursos de la orden {order.OrderId}: {ex.Message}");
                }
            }
            
            try
            {
                Console.WriteLine("Eliminando órdenes expiradas...");
                await _command.DeleteRangeAsync(expiredOrders, cancellationToken);
                Console.WriteLine("Órdenes eliminadas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error eliminando órdenes expiradas: " + ex.Message);
            }

            return Unit.Value;
        }
    }
}
