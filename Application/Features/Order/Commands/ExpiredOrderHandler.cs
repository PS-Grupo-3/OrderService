using Application.Interfaces.Command;
using Application.Interfaces.Query;
using MediatR;


namespace Application.Features.Order.Commands
{
    public class ExpiredOrderHandler : IRequestHandler<ExpiredOrderCommand, Unit>
    {
        private readonly IOrderCommand orderCommand;
        private readonly IOrderQuery orderQuery;

        public ExpiredOrderHandler(IOrderCommand orderCommand, IOrderQuery orderQuery)
        {
            this.orderCommand = orderCommand;
            this.orderQuery = orderQuery;
        }

        public async Task<Unit> Handle(ExpiredOrderCommand request, CancellationToken cancellationToken)
        {

            var expiredOrders = await orderQuery.GetExpiredOrders(cancellationToken);
            if (expiredOrders == null) 
            {
                return Unit.Value;
                
            }
            foreach (var order in expiredOrders) 
            {
                await orderCommand.DeleteAsync(order);
            }
            return Unit.Value;
            
        }
    }
}
