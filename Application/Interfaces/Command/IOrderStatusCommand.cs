using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IOrderStatusCommand
    {
        Task InsertAsync(OrderStatus entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(OrderStatus entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(OrderStatus entity, CancellationToken cancellationToken = default);
    }
}
