

using Application.Interfaces.Order;
using Infrastructure.Persistence;

namespace Infrastructure.Commands.Order
{
    public class OrderCommand:IOrderCommand
    {
        private readonly AppDbContext _context;

        public OrderCommand(AppDbContext context)
        {
            _context = context;
        }
    }
}
