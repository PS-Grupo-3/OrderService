using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly AppDbContext _context;

        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(DateTime? from, DateTime? to, int? status, CancellationToken cancellationToken = default)
        {
            var query = _context.Orders
                .Include(o => o.PaymentStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(o => o.BuyDate >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(o => o.BuyDate <= to.Value);
            }
            if (status.HasValue)
            {
                query = query.Where(o => o.OrderStatusId == status.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.PaymentStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(order=>order.OrderId==orderId, cancellationToken);
        }

    }
}
