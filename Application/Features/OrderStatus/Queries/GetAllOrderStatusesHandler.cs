using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public class GetAllOrderStatusesHandler : IRequestHandler<GetAllOrderStatusesQuery, List<GenericResponse>>
    {
        private readonly IOrderStatusQuery _query;

        public GetAllOrderStatusesHandler(IOrderStatusQuery query)
        {
            _query = query;
        }

        public async Task<List<GenericResponse>> Handle(GetAllOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _query.GetAllAsync(cancellationToken);

            return statuses.Select(Payment => new GenericResponse
            {
                Id = Payment.OrderStatusId,
                Name = Payment.OrderStatusName
            }).ToList();
        }
    }
}
