using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public record GetPaymentTypeByIdQuery(int paymentTypeId) : IRequest<GenericResponse>;
}
