using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands
{
    public class OrderCommand: IOrderCommand
    {
        private readonly AppDbContext _context;

        public OrderCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Order entity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order entity, CancellationToken cancellationToken = default)
        {
            _context.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;

            // Marcar los nuevos detalles como agregados
            foreach (var detail in entity.OrderDetails)
            {
                if (!_context.OrderDetails.Any(d => d.DetailId == detail.DetailId))
                {
                    _context.Entry(detail).State = EntityState.Added;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<Order> Entities, CancellationToken cancellationToken = default)
        { 
            _context.Orders.RemoveRange(Entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
