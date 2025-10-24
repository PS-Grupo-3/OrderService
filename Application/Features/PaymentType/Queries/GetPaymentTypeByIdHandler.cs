using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public class GetPaymentTypeByIdHandler : IRequestHandler<GetPaymentTypeByIdQuery, GenericResponse>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetPaymentTypeByIdHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<GenericResponse> Handle(GetPaymentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _Query.GetByIdAsync(request.paymentTypeId, cancellationToken);

            if (payment is null)
            {
                throw new ArgumentNullException($"No se encontró el método de pago con el ID {request.paymentTypeId}");
            }

            return new GenericResponse
            {
                Id = payment.PaymentId,
                Name = payment.PaymentName
            };
        }
    }
}
