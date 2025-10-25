using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class CreateOrderHandler:IRequestHandler<CreateOrderCommand,OrderResponse>
    {
        private readonly IOrderCommand _Command;
        private readonly IPaymentTypeQuery _PaymentType;
        public CreateOrderHandler(IOrderCommand command, IPaymentTypeQuery PaymentType)
        {
            _Command = command;
            _PaymentType = PaymentType;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var PaymentType = await _PaymentType.GetByIdAsync(request.request.PaymentId);

            if (PaymentType == null) {
                throw new KeyNotFoundException($"No existe un método de pago con ID {request.request.PaymentId}");
            }

            double totalAmount = 0;
            var orderDetails = new List<OrderDetail>();
            
            foreach (var detail in request.request.Details)
            {
                if (detail.UnitPrice < 0)
                {
                    throw new ArgumentException($"El precio {detail.UnitPrice} no es válido.");
                }
                
                if (detail.Quantity < 0)
                {
                    throw new ArgumentException($"La cantidad {detail.Quantity} no es válido.");
                }

                double subTotal = detail.UnitPrice * detail.Quantity;
                totalAmount += subTotal;

                orderDetails.Add(new OrderDetail
                {
                    TicketId = detail.TicketId,
                    UnitPrice = detail.UnitPrice,
                    Quantity = detail.Quantity,
                    Subtotal = subTotal
                });
            }

            var order = new Domain.Entities.Order
            {
                UserId = request.request.UserId,
                BuyDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                PaymentId = request.request.PaymentId,
                PaymentStatusId = 1,
                OrderStatusId = 1,
                OrderDetails = orderDetails
            };

            await _Command.InsertAsync(order);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                CreateAt = order.BuyDate,
                TotalAmount = order.TotalAmount
            };
           
        }
    }
}
