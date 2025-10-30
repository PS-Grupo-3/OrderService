using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class OrderDetailQuery : IOrderDetailQuery
    {
        private readonly AppDbContext _context;

        public OrderDetailQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDetail>> GetOrderDetailsByOrderId(Guid OrderId, CancellationToken cancellationToken = default)
        {
            return await _context.OrderDetails.Where(od => od.OrderId == OrderId).ToListAsync();
        }
    }
}
