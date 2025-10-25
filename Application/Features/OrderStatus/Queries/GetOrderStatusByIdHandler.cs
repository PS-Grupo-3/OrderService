using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.OrderStatus.Queries
{
    public class GetOrderStatusByIdHandler : IRequestHandler<GetOrderStatusByIdQuery, GenericResponse>
    {
        private readonly IOrderStatusQuery _Query;

        public GetOrderStatusByIdHandler(IOrderStatusQuery query)
        {
            _Query = query;
        }

        public async Task<GenericResponse> Handle(GetOrderStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.orderStatusId <= 0)
            {
                throw new ArgumentException($"El método de pago con el ID {request.orderStatusId} es inválido");
            }

            var status = await _Query.GetByIdAsync(request.orderStatusId, cancellationToken);

            if (status is null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de la órden con el ID {request.orderStatusId} es onválido");
            }

            return new GenericResponse
            {
                Id = status.OrderStatusId,
                Name = status.StatusName
            };
        }
    }
}
