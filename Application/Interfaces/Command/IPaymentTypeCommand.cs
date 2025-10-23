using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IPaymentTypeCommand
    {
        Task InsertAsync(PaymentType entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(PaymentType entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(PaymentType entity, CancellationToken cancellationToken = default);

    }
}
