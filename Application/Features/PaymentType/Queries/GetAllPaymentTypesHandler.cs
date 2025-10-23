using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public class GetAllPaymentTypesHandler : IRequestHandler<GetAllPaymentTypesQuery,List<PaymentResponse>>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetAllPaymentTypesHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<List<PaymentResponse>> Handle(GetAllPaymentTypesQuery request, CancellationToken cancellationToken) 
        {
            var payments = await _Query.GetAllAsync(cancellationToken);

            return payments.Select(Payment => new PaymentResponse
            {
                Id=Payment.PaymentId,
                PaymentName=Payment.PaymentName
            }).ToList();
        }
    }
}
