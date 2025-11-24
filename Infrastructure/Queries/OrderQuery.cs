using Application.Interfaces.Query;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly AppDbContext _context;

        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(DateTime? from, DateTime? to, int? payment, Guid? userId, CancellationToken cancellationToken = default)
        {
            var query = _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(o => o.CreatedAt >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(o => o.CreatedAt <= to.Value);
            }
            if (payment.HasValue)
            {
                query = query.Where(o => o.PaymentId == payment.Value);
            }
            if (userId.HasValue)
            {
                query = query.Where(o => o.UserId == userId.Value);
            }

            query = query.Where(o => o.OrderStatus.OrderStatusName == OrderStatusNames.Paid);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(order => order.OrderId == orderId, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetExpiredOrders(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            return await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDetails)
                .Where(o =>
                    o.OrderStatus.OrderStatusName != OrderStatusNames.Paid &&
                    o.ExpirationDate <= now)
                .ToListAsync(cancellationToken);
        }


    }
}
