

using Domain.Entities;

namespace Application.Interfaces.Payment
{
    public interface IPaymentQuery
    {
        Task<IEnumerable<PaymentType>> GetAllPaymentTypes();
    }
}
