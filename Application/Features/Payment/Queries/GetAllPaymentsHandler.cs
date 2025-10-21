using Application.Interfaces.Payment;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Payment.Queries
{
    public class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsQuery,List<PaymentTypeResponse>>
    {
        private readonly IPaymentQuery _Query;

        public GetAllPaymentsHandler(IPaymentQuery query)
        {
            _Query = query;
        }

        public async Task<List<PaymentTypeResponse>> Handle(GetAllPaymentsQuery request,CancellationToken cancellationToken) 
        {
            var payments = await _Query.GetAllPaymentTypes();

            return payments.Select(Payment => new PaymentTypeResponse
            {
                Id=Payment.PaymentId,
                PaymentName=Payment.PaymentName,
            }).ToList();
        }


    }
}
