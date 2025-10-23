using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderStatusQuery
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderStatus?> GetByIdAsync(int orderStatusId, CancellationToken cancellationToken = default);
    }
}
