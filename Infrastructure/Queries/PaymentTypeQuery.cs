using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class PaymentTypeQuery : IPaymentTypeQuery
    {
        private readonly AppDbContext _context;

        public PaymentTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.PaymentTypes.ToListAsync(cancellationToken);
        }

        public async Task<PaymentType?> GetByIdAsync(int paymentTypeId, CancellationToken cancellationToken = default)
        {
            return await _context.PaymentTypes
                .FirstOrDefaultAsync(c => c.PaymentId == paymentTypeId, cancellationToken);
        }
    }
}
