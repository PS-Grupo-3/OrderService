using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class OrderStatusCommand : IOrderStatusCommand
    {
        private readonly AppDbContext _context;

        public OrderStatusCommand(AppDbContext context)
        { 
            _context = context;
        }

        public async Task InsertAsync(OrderStatus entity, CancellationToken cancellationToken = default)
        {
            await _context.OrderStatuses.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(OrderStatus entity, CancellationToken cancellationToken = default)
        {
            _context.OrderStatuses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(OrderStatus entity, CancellationToken cancellationToken = default)
        {
            _context.OrderStatuses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
