
using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderQuery
    {
        Task<Order> GetOrderByIdAsync(Guid orderId,CancellationToken cancellationToken=default);

    }
}
