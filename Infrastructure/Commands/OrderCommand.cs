using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

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
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Order entity, CancellationToken cancellationToken = default)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Order>UpdateOrderPaymentStatus(Order Entity, int newPaymentStatus ,CancellationToken cancellationToken = default)
        {
            Entity.PaymentStatusId = newPaymentStatus;
            switch (newPaymentStatus)
            {
                case 1:
                    Entity.OrderStatusId = 1;
                    break;
                case 2:
                    Entity.OrderStatusId = 2;
                    break;
                case 3:
                    Entity.OrderStatusId = 3;
                    break;
            }
            _context.Orders.Update(Entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Entity;
        }
    }
}
