using Application.Interfaces.Command;
using Application.Interfaces.Query;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class ExpiredOrderHandler : IRequestHandler<ExpiredOrderCommand, Unit>
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;

        public ExpiredOrderHandler(IOrderCommand command, IOrderQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<Unit> Handle(ExpiredOrderCommand request, CancellationToken cancellationToken)
        {
            var expiredOrders = await _query.GetExpiredOrders(cancellationToken);
            if (expiredOrders == null) 
            {
                return Unit.Value;
                
            }
            foreach (var order in expiredOrders) 
            {
                await _command.DeleteAsync(order);
            }
            return Unit.Value;
        }
    }
}
