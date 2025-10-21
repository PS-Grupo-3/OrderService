

using Application.Interfaces.Payment;
using Infrastructure.Persistence;

namespace Infrastructure.Commands.Payment
{
    public class PaymentCommand:IPaymentCommand
    {
        private readonly AppDbContext _context;

        public PaymentCommand(AppDbContext context)
        {
            _context = context;
        }
    }
}
