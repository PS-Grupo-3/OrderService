using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public class GetPaymentStatusByIdHandler : IRequestHandler<GetPaymentStatusByIdQuery, GenericResponse>
    {
        private readonly IPaymentStatusQuery _Query;

        public GetPaymentStatusByIdHandler(IPaymentStatusQuery query)
        {
            _Query = query;
        }

        public async Task<GenericResponse> Handle(GetPaymentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.paymentStatusId <= 0)
            {
                throw new ArgumentException($"El estado del método de pago con el ID {request.paymentStatusId} es inválido");
            }

            var payment = await _Query.GetByIdAsync(request.paymentStatusId, cancellationToken);

            if (payment is null)
            {
                throw new KeyNotFoundException($"No se encontró el estado del método de pago con el ID {request.paymentStatusId}");
            }

            return new GenericResponse
            {
                Id = payment.PaymentStatusId,
                Name = payment.PaymentStatusName
            };
        }
    }
}
