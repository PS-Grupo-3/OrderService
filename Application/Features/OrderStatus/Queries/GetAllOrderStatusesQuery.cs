using Application.Models.Responses;
using MediatR;

namespace Application.Features.OrderStatus.Queries
{
    public record GetAllOrderStatusesQuery() : IRequest<List<GenericResponse>>;
}
