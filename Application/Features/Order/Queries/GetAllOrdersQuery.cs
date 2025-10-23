using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Queries
{
    public record class GetAllOrdersQuery(DateTime? from, DateTime? to, int? status) : IRequest<List<CompleteOrderResponse>>;
}
