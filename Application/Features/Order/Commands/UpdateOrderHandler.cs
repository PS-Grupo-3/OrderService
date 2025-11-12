using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;

        public UpdateOrderHandler(IOrderCommand command, IOrderQuery query, IPaymentTypeQuery queryPayment)
        {
            _command = command;
            _query = query;
        }

        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _query.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"No se encontró la orden con el ID {request.Id}");
            }

            if (order.OrderStatus.OrderStatusName == OrderStatusNames.Paid)
            {
                throw new ConflictException("No se puede modificar una orden pagada.");
            }

            var newDetails = request.request.Details.ToDictionary(d => d.SectorId, d => d);

            var toRemove = new List<OrderDetail>();
            foreach (var existing in order.OrderDetails)
            {
                if (newDetails.TryGetValue(existing.SectorId, out var updated))
                {
                    if (updated.UnitPrice <= 0)
                    {
                        throw new ArgumentException($"El precio ${updated.UnitPrice} no es válido.");
                    }

                    if (updated.Quantity <= 0)
                    {
                        throw new ArgumentException($"La cantidad {updated.Quantity} no es válido.");
                    }
                    if (updated.Tax < 0 || updated.Tax > 100)
                    {
                        throw new ArgumentException("El impuesto debe estar entre 0 y 100.");
                    }
                    if (updated.Discount < 0 || updated.Discount > 100)
                    {
                        throw new ArgumentException("El descuento debe estar entre 0 y 100.");
                    }

                    existing.Quantity = updated.Quantity;
                    existing.UnitPrice = updated.UnitPrice;

                    existing.Subtotal = updated.Quantity * updated.UnitPrice;

                    existing.DiscountAmount = existing.Subtotal * ((updated.Discount ?? 0) / 100);
                    existing.TaxAmount = existing.Subtotal * ((updated.Tax ?? 0) / 100);

                    existing.Total = decimal.Round(existing.Subtotal - existing.DiscountAmount + existing.TaxAmount, 2, MidpointRounding.AwayFromZero);

                    existing.UpdatedAt = DateTime.UtcNow;

                    newDetails.Remove(existing.SectorId);
                }
                else
                {
                    toRemove.Add(existing);
                }
            }

            foreach (var d in toRemove)
            {
                order.OrderDetails.Remove(d);
            }

            // Agregar los nuevos
            foreach (var item in newDetails.Values)
            {
                if (item.UnitPrice <= 0)
                {
                    throw new ArgumentException($"El precio ${item.UnitPrice} no es válido.");
                }

                if (item.Quantity <= 0)
                {
                    throw new ArgumentException($"La cantidad {item.Quantity} no es válido.");
                }
                if (item.Tax < 0 || item.Tax > 100)
                {
                    throw new ArgumentException("El impuesto debe estar entre 0 y 100.");
                }
                if (item.Discount < 0 || item.Discount > 100)
                {
                    throw new ArgumentException("El descuento debe estar entre 0 y 100.");
                }

                var subTotal = item.Quantity * item.UnitPrice;
                var discountAmount = subTotal * ((item.Discount ?? 0) / 100);
                var taxAmount = subTotal * ((item.Tax ?? 0) / 100);

                order.OrderDetails.Add(new OrderDetail
                {
                    DetailId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    SectorId = item.SectorId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Subtotal = subTotal,
                    DiscountAmount = discountAmount,
                    TaxAmount = taxAmount,
                    Total = decimal.Round(subTotal - discountAmount + taxAmount, 2, MidpointRounding.AwayFromZero),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            // Calcular total y actualizar estado
            order.TotalAmount = order.OrderDetails.Sum(d => d.Total);

            order.UpdatedAt = DateTime.UtcNow;

            await _command.UpdateAsync(order, cancellationToken);

            return new UpdatedOrderResponse
            {
                OrderId = order.OrderId,
                Event = order.EventId,
                Venue = order.VenueId,
                UpdatedAt = order.CreatedAt,
                ExpiresAt = order.ExpirationDate,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
