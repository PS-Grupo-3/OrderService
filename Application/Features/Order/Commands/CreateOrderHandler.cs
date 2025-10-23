using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System.Windows.Input;

namespace Application.Features.Order.Commands
{
    public class CreateOrderHandler:IRequestHandler<CreateOrder,OrderResponse>
    {
        private readonly IOrderCommand _Command;
        private readonly IPaymentTypeQuery _PaymentType;
        private readonly IPaymentStatusQuery _PaymentStatus;
        private readonly IOrderStatusQuery _OrderStatusQuery;
        public CreateOrderHandler(IOrderCommand command, IPaymentTypeQuery PaymentType, IPaymentStatusQuery PaymentStatus, IOrderStatusQuery orderStatusQuery)
        {
            _Command = command;
            _PaymentType = PaymentType;
            _PaymentStatus = PaymentStatus;
            _OrderStatusQuery = orderStatusQuery;
        }

        public async Task<OrderResponse> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var PaymentType = await _PaymentType.GetByIdAsync(request.request.PaymentId);
            if (PaymentType == null) {
                //Validación
            }
            
            var order = new Domain.Entities.Order
            {
                OrderId = new Guid(),
                UserId = request.request.UserId,
                BuyDate = DateTime.UtcNow,
                TotalAmount = 2400,//Este precio lo pongo para pruebas
                PaymentId= request.request.PaymentId,
                PaymentStatusId= 1, //Lo pongo así porque inicialmente va a estar pendiente.
                OrderStatusId=1, //Lo mismo con el estado de la orden. Va a estar pendiente hasta que se realice el pago.
            };
            var paymentStatus = await _PaymentStatus.GetByIdAsync(order.PaymentStatusId);
            var orderStatus = await _OrderStatusQuery.GetByIdAsync(order.OrderStatusId);
            await _Command.InsertAsync(order);

            return new OrderResponse 
            {
            OrderId=order.OrderId,
            UserId=order.UserId,
            TotalAmount=order.TotalAmount,
            Payment= new PaymentResponse 
            {
            Id=order.PaymentId,
            PaymentName=PaymentType.PaymentName
            },
            PaymentStatus= new PaymentStatusResponse
            {
                statusId = order.PaymentStatusId,
                StatusName = paymentStatus.PaymentStatusName
            },
            OrderStatus = new OrderStatusResponse 
            {
            Id= orderStatus.OrderStatusId,
            StatusName= orderStatus.StatusName,
            }
            
            };
            
        }
    }
}
