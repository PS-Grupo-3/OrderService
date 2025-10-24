using Application.Exceptions;
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
            var order = await query.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException404($"No se encontró la orden con el id {request.Id}");
            }

            if (request.request.Status <= 0 || request.request.Status > 3)
            {
                throw new ArgumentException("Estado inválido");
            }

            if (request.request.Status < order.PaymentStatusId) 
            {
                throw new BadRequestException400($"No se puede actualizar a un estado previo");
            }
            
            await command.UpdateOrderPaymentStatus(order, request.request.Status, cancellationToken);

            //Lo actualizo.
            var newOrder = await query.GetByIdAsync(order.OrderId, cancellationToken);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                CreateAt = order.BuyDate,
                TotalAmount = order.TotalAmount
            };
         
        }
    }
}
