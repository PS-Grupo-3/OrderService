using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderDetailQuery
    {
        Task<IEnumerable<OrderDetail>> GetAllByIdAsync(List<Guid> detailsId, CancellationToken cancellationToken = default);
    }
}
