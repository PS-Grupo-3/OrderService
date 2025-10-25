using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderQuery
    {
        Task<IEnumerable<Order>> GetAllAsync(DateTime? from, DateTime? to, int? status, Guid? userId, CancellationToken cancellationToken = default);
        Task<Order> GetByIdAsync(Guid orderId,CancellationToken cancellationToken=default);

    }
}
