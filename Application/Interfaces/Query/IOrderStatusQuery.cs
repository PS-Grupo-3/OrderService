using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderStatusQuery
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderStatus> GetByIdAsync(int paymentStatusId, CancellationToken cancellationToken = default);
    }
}
