using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderPaymentStatusHandler: IRequestHandler<UpdateOrderPaymentStatusCommand,OrderResponse>
    {
        private readonly IOrderCommand command;
        private readonly IOrderQuery query;
        private readonly IOrderStatusQuery _OrderStatusQuery;
        public UpdateOrderPaymentStatusHandler(IOrderCommand command, IOrderQuery _query, IOrderStatusQuery OrderStatusQuery)
        {
            this.command = command;
            this.query = _query;
            this._OrderStatusQuery = OrderStatusQuery;

        }

        public async Task<OrderResponse> Handle(UpdateOrderPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await query.GetOrderByIdAsync(request.request.OrderId,cancellationToken);

            if (order == null)
            {
                //Validación
            }
            
             await command.UpdateOrderPaymentStatus(order, request.request.PaymentStatusId,cancellationToken);
            //Lo actualizo.
            var newOrder = await query.GetOrderByIdAsync(order.OrderId, cancellationToken);
            //Lo traigo con todas los campos.
            var status = await _OrderStatusQuery.GetByIdAsync(order.OrderStatusId);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                CreateAt = order.BuyDate,
                TotalAmount = order.TotalAmount
            };
            /*return new OrderResponse
                {
                    OrderId = newOrder.OrderId,
                    UserId = newOrder.UserId,
                    TotalAmount = newOrder.TotalAmount,
                    Payment = new PaymentResponse
                    {
                        Id = newOrder.PaymentId,
                        PaymentName = newOrder.PaymentType.PaymentName,
                    },
                    PaymentStatus = new PaymentStatusResponse
                    {
                        statusId=newOrder.PaymentStatusId,
                        StatusName=newOrder.PaymentStatus.PaymentStatusName,
                    },
                    OrderStatus=new OrderStatusResponse 
                    {
                    Id=status.OrderStatusId,
                    StatusName = status.StatusName,
                    }
                };*/
        }
    }
}
