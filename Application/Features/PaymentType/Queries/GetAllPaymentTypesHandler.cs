using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public class GetAllPaymentTypesHandler : IRequestHandler<GetAllPaymentTypesQuery,List<GenericResponse>>
    {
        private readonly IPaymentTypeQuery _Query;

        public GetAllPaymentTypesHandler(IPaymentTypeQuery query)
        {
            _Query = query;
        }

        public async Task<List<GenericResponse>> Handle(GetAllPaymentTypesQuery request, CancellationToken cancellationToken) 
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
