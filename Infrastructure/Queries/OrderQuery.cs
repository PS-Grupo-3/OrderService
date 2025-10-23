using Application.Interfaces.Query;
using Infrastructure.Persistence;

namespace Infrastructure.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly AppDbContext _context;

        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }
    }
}
