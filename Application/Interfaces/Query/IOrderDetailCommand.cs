
using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderDetailCommand
    {
        Task<OrderDetail> updateTransactionIdAsync(OrderDetail orderdetail, string transactionId, CancellationToken cancellationToken = default);
    }
}
