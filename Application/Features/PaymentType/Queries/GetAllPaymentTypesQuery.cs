using Application.Models.Responses;
using MediatR;

namespace Application.Features.PaymentType.Queries
{
    public record GetAllPaymentTypesQuery() : IRequest<List<PaymentResponse>>;
}
