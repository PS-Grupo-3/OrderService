
using Application.Interfaces.Payment;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.Payment
{
    public class PaymentQuery:IPaymentQuery
    {
        private readonly AppDbContext _context;

        public PaymentQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypes()
        {
            return await _context.PaymentTypes.ToListAsync();
        }
    }
}
