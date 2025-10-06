using Application.Interface;
using Infraestructure.Persistence;

namespace Infraestructure.Queries
{
    public class PaymentQueries:IPaymentQuery
    {
        private readonly AppDbContext Context;

        public PaymentQueries(AppDbContext context)
        {
            Context = context;
        }
    }
}
