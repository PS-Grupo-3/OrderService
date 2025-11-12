using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public record GetAllOrderStatusesQuery() : IRequest<List<GenericResponse>>;
}
