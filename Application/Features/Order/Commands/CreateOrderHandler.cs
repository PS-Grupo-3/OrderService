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
            if (string.IsNullOrEmpty(request.request.Event)) 
            {
                throw new ArgumentException($"Se requiere un nombre de evento");
            }
            if (string.IsNullOrEmpty(request.request.Address))
            {
                throw new ArgumentException($"Se requiere una dirección");
            }
            if (request.request.EventDate==null) 
            {
                throw new ArgumentException($"Se requiere una fecha de evento");

            }
            var order = new Domain.Entities.Order
            {
                OrderId = new Guid(),
                UserId = request.request.UserId,
                EventName = request.request.Event,
                EventDate = request.request.EventDate,
                VenueName = request.request.Venue,
                VenueAddress = request.request.Address,
                TotalAmount = 0,
                Currency = "ARS",
                PaymentId = 1,
                PaymentStatusId = 1,
                CreatedAt = DateTime.UtcNow,
                PaymentDate = null,
            };

            await _command.InsertAsync(order);

            return new CreatedOrderResponse
            {
                OrderId = order.OrderId,
                Event = order.EventName,
                EventDate = order.EventDate,
                Venue = order.VenueName,
                Address = order.VenueAddress,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
