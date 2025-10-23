using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class PaymentStatusQuery : IPaymentStatusQuery
    {
        private readonly AppDbContext _context;

        public PaymentStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.PaymentStatuses.ToListAsync(cancellationToken);
        }

        public async Task<PaymentStatus?> GetByIdAsync(int paymentStatusId, CancellationToken cancellationToken = default)
        {
            return await _context.PaymentStatuses
                .FirstOrDefaultAsync(c => c.PaymentStatusId == paymentStatusId, cancellationToken);
        }
    }
}
