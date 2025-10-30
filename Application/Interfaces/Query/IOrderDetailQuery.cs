
using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderDetailQuery
    {
        Task<List<OrderDetail>> GetOrderDetailsByOrderId(Guid OrderDetailId,CancellationToken cancellationToken = default);

    }
}
