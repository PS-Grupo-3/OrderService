using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class PaymentTypeCommand: IPaymentTypeCommand
    {
        private readonly AppDbContext _context;

        public PaymentTypeCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(PaymentType entity, CancellationToken cancellationToken = default)
        {
            await _context.PaymentTypes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PaymentType entity, CancellationToken cancellationToken = default)
        {
            _context.PaymentTypes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PaymentType entity, CancellationToken cancellationToken = default)
        {
            _context.PaymentTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
