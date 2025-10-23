using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class OrderStatusQuery : IOrderStatusQuery
    {
        private readonly AppDbContext _context;

        public OrderStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrderStatuses.ToListAsync(cancellationToken);
        }

        public async Task<OrderStatus?> GetByIdAsync(int orderStatusId, CancellationToken cancellationToken = default)
        {
            return await _context.OrderStatuses
                .FirstOrDefaultAsync(c => c.OrderStatusId == orderStatusId, cancellationToken);
        }
    }
}
