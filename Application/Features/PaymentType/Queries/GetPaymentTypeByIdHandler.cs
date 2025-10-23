using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public class GetPaymentTypeByIdHandler : IRequestHandler<GetPaymentTypeByIdQuery, PaymentResponse>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetPaymentTypeByIdHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<PaymentResponse> Handle(GetPaymentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _Query.GetByIdAsync(request.paymentTypeId, cancellationToken);

            if (payment is null)
            {
                throw new ArgumentNullException($"No se encontró el método de pago con el ID {request.paymentTypeId}");
            }

            return new PaymentResponse
            {
                Id = payment.PaymentId,
                PaymentName = payment.PaymentName
            };
        }
    }
}
