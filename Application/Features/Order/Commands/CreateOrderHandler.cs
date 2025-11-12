using Application.Interfaces.Command;
using Application.Models.Responses;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        private readonly IOrderCommand _command;
        public CreateOrderHandler(IOrderCommand command)
        {
            _command = command;
        }

        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order
            {
                OrderId = Guid.NewGuid(),
                UserId = request.request.UserId,
                EventId = request.request.Event,
                VenueId = request.request.Venue,
                TotalAmount = 0,
                Currency = "ARS",
                PaymentId = null,
                OrderStatusId = OrderStatusIds.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PaymentDate = null,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5)
            };

            await _command.InsertAsync(order);

            return new CreatedOrderResponse
            {
                OrderId = order.OrderId,
                Event = order.EventId,
                Venue = order.VenueId,
                CreatedAt = order.CreatedAt,
                ExpiresAt = order.ExpirationDate
            };
        }
    }
}
