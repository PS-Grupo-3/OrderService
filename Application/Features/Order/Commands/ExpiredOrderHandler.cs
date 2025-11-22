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

        public ExpiredOrderHandler(IOrderCommand command, IOrderQuery query, IEventServiceClient eventServiceClient)
        {
            _command = command;
            _query = query;
            _eventServiceClient = eventServiceClient;
        }
        
        public async Task<Unit> Handle(ExpiredOrderCommand request, CancellationToken cancellationToken)
        {
            var expiredOrders = await _query.GetExpiredOrders(cancellationToken);

            foreach (var order in expiredOrders)
            {
                var seats = order.OrderDetails
                    .Where(d => d.IsSeat && d.EventSeatId.HasValue)
                    .Select(d => d.EventSeatId!.Value)
                    .ToList();

                if (seats.Any())
                {
                    await _eventServiceClient.MarkSeatsAsAvailableAsync(order.EventId, seats, cancellationToken);
                }
                
                // Como es logica de negocio, lo pongo acá y no en una query de Infrastructure (aviso para vos especialmente ALVARO)
                var sectors = order.OrderDetails
                    .Where(d => !d.IsSeat)
                    .Select(d => d.EventSectorId)
                    .Distinct()
                    .ToList();

                foreach (var sectorId in sectors)
                {
                    await _eventServiceClient.MarkSectorAsAvailableAsync(sectorId, cancellationToken);
                }
            }

            await _command.DeleteRangeAsync(expiredOrders, cancellationToken);

            return Unit.Value;
        }
    }
}

