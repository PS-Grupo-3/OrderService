using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class DeleteOrderHandler:IRequestHandler<DeleteOrderCommand,OrderResponse>
    {
        private readonly IOrderCommand Ordercommand;
        private readonly IOrderQuery Orderquery;

        public DeleteOrderHandler(IOrderCommand _command, IOrderQuery _query)
        {
            this.Ordercommand = _command;
            this.Orderquery = _query;
        }

        public async Task<OrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var ExistOrder = await Orderquery.GetByIdAsync(request.orderId, cancellationToken);
            if (ExistOrder == null)
            {
                throw new KeyNotFoundException($"No se encontró la orden con el id {request.orderId}");
            }
            else 
            {
                if (ExistOrder.OrderStatusId != 1)
                {
                    throw new KeyNotFoundException($"El estado de la orden no es {request.orderId} no es pendiente");
                }
                
                await Ordercommand.DeleteAsync(ExistOrder,cancellationToken);
                   
                    return new OrderResponse 
                    {
                        OrderId=ExistOrder.OrderId,
                        TotalAmount= ExistOrder.TotalAmount,
                        CreateAt=ExistOrder.BuyDate,
                    };
                
            }

        }
    }
}
