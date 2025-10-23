using Application.Features.PaymentStatus.Queries;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderStatus.Queries
{
    public class GetAllOrderStatusesHandler : IRequestHandler<GetAllOrderStatusesQuery, List<OrderStatusResponse>>
    {
        private readonly IOrderStatusQuery _Query;

        public GetAllOrderStatusesHandler(IOrderStatusQuery query)
        {
            _Query = query;
        }

        public async Task<List<OrderStatusResponse>> Handle(GetAllOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            var status = await _Query.GetAllAsync(cancellationToken);

            return status.Select(Status => new OrderStatusResponse
            {
                Id = Status.OrderStatusId,
                StatusName = Status.StatusName
            }).ToList();
        }
    }
}
