using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public record GetOrderStatusByIdQuery(int orderStatusId) : IRequest<GenericResponse>;
}
