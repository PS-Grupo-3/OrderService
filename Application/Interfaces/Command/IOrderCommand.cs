using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IOrderCommand
    {
        Task InsertAsync(Order entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<Order> entities, CancellationToken cancellationToken = default);
        
    }
}
