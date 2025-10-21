using Application.Models.Responses;
using MediatR;

namespace Application.Features.Payment.Queries
{
        public record GetAllPaymentsQuery() : IRequest<List<PaymentTypeResponse>>;

 
}
