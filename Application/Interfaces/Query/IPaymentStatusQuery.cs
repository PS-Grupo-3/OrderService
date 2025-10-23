using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IPaymentStatusQuery
    {
        Task<IEnumerable<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaymentStatus?> GetByIdAsync(int paymentStatusId, CancellationToken cancellationToken = default);
    }
}
