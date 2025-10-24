using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public class GetAllPaymentStatusesHandler : IRequestHandler<GetAllPaymentStatusesQuery, List<GenericResponse>>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetAllPaymentStatusesHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<List<GenericResponse>> Handle(GetAllPaymentStatusesQuery request, CancellationToken cancellationToken)
        {
            var payments = await _Query.GetAllAsync(cancellationToken);

            return payments.Select(Payment => new GenericResponse
            {
                Id = Payment.PaymentId,
                Name = Payment.PaymentName
            }).ToList();
        }
    }
}
