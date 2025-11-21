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
            var dto = request.request;

            var order = new Domain.Entities.Order
            {
                OrderId = Guid.NewGuid(),
                UserId = dto.UserId,
                EventId = dto.Event,
                VenueId = dto.Venue,
                Currency = "ARS",
                PaymentId = null,
                TotalAmount = 0,
                OrderStatusId = OrderStatusIds.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PaymentDate = null,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                OrderDetails = new List<OrderDetail>()
            };

            decimal total = 0;
            if (dto.Seats != null)
            {
                foreach (var seat in dto.Seats)
                {
                    var detail = new OrderDetail
                    {
                        DetailId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        EventId = seat.EventId,
                        EventSectorId = seat.EventSectorId,
                        EventSeatId = seat.EventSeatId,
                        IsSeat = true,
                        Quantity = 1,
                        UnitPrice = seat.Price,
                        Subtotal = seat.Price,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    order.OrderDetails.Add(detail);
                    total += seat.Price;
                }
            }
            
            if (dto.Sectors != null)
            {
                foreach (var sector in dto.Sectors)
                {
                    var subTotal = sector.UnitPrice * sector.Quantity;
                    var detail = new OrderDetail
                    {
                        DetailId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        EventId = sector.EventId,
                        EventSectorId = sector.EventSectorId,
                        EventSeatId = null, // no applica en sectores no controlados
                        IsSeat = false,
                        Quantity = sector.Quantity,
                        UnitPrice = sector.UnitPrice,
                        Subtotal = subTotal,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    order.OrderDetails.Add(detail);
                    total += subTotal;
                }
            }

            order.TotalAmount = total;
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
