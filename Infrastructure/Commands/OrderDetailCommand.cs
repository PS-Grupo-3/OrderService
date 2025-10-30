

using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class OrderDetailCommand : IOrderDetailCommand
    {

        private readonly AppDbContext _context;

        public OrderDetailCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDetail> updateTransactionIdAsync(OrderDetail orderdetail,string newtransactionId, CancellationToken cancellationToken = default)
        {
            orderdetail.transactionId = newtransactionId;
            await _context.SaveChangesAsync(cancellationToken);
            return orderdetail;
        }
    }
}
