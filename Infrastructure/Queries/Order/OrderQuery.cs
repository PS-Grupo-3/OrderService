

using Application.Interfaces.Order;
using Infrastructure.Persistence;

namespace Infrastructure.Queries.Order
{
    public class OrderQuery:IOrderQuery
    {
        private readonly AppDbContext _context;

        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }
    }
}
