using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentStatus.Queries
{
    public record GetPaymentStatusByIdQuery(int paymentStatusId) : IRequest<GenericResponse>;
}
