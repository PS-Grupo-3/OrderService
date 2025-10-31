using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, CreatedOrderResponse>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;

        public DeleteOrderHandler(IOrderCommand command, IOrderQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<CreatedOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _query.GetByIdAsync(request.orderId, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"No se encontró la orden con el ID {request.orderId}.");
            }

            if (order.PaymentStatusId == 2)
            {
                throw new ConflictException($"No se puede eliminar una orden con estado pagado.");
            }

            await _command.DeleteAsync(order, cancellationToken);

            return new CreatedOrderResponse
            {
                Id = order.OrderId,
                Event = order.EventName,
                EventDate = order.EventDate,
                Venue = order.VenueName,
                Address = order.VenueAddress,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
