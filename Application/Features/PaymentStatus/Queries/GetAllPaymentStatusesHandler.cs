using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public class GetAllPaymentStatusesHandler : IRequestHandler<GetAllPaymentStatusesQuery, List<PaymentResponse>>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetAllPaymentStatusesHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<List<PaymentResponse>> Handle(GetAllPaymentStatusesQuery request, CancellationToken cancellationToken)
        {
            var payments = await _Query.GetAllAsync(cancellationToken);

            return payments.Select(Payment => new PaymentResponse
            {
                Id = Payment.PaymentId,
                PaymentName = Payment.PaymentName
            }).ToList();
        }
    }
}
