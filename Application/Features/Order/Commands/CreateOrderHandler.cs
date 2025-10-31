using Application.Interfaces.Command;
using Application.Models.Responses;
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
                UserId = request.request.UserId,
                EventName = request.request.Event,
                EventDate = request.request.EventDate,
                VenueName = request.request.Venue,
                VenueAddress = request.request.Address,
                TotalAmount = 0,
                Currency = "ARS",
                PaymentId = 0,
                PaymentStatusId = 1,
                CreatedAt = DateTime.UtcNow,
                PaymentDate = null
            };

            await _command.InsertAsync(order);

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
