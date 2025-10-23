using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IPaymentTypeQuery
    {
        Task<IEnumerable<PaymentType>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaymentType?> GetByIdAsync(int paymentTypeId, CancellationToken cancellationToken = default);
    }
}
