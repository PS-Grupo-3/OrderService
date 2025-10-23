using Application.Interfaces.Query;
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

        public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o=>o.PaymentStatus).Include(o => o.PaymentType).FirstOrDefaultAsync(order=>order.OrderId==orderId);
        }
    }
}
