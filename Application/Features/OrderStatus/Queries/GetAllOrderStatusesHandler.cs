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
    public class GetAllOrderStatusesHandler : IRequestHandler<GetAllOrderStatusesQuery, List<GenericResponse>>
    {
        private readonly IOrderStatusQuery _Query;

        public GetAllOrderStatusesHandler(IOrderStatusQuery query)
        {
            _Query = query;
        }

        public async Task<List<GenericResponse>> Handle(GetAllOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            var status = await _Query.GetAllAsync(cancellationToken);

            return status.Select(Status => new GenericResponse
            {
                Id = Status.OrderStatusId,
                Name = Status.StatusName
            }).ToList();
        }
    }
}
