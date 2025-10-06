using Application.Interface;
using Infraestructure.Persistence;
namespace Infraestructure.Command
{
    public class PaymentCommand:IPaymentCommand
    {
        private readonly AppDbContext Context;

        public PaymentCommand(AppDbContext context)
        {
            Context = context;
        }
    }
}
