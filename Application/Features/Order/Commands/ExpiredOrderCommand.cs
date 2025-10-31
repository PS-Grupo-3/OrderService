using MediatR;

namespace Application.Features.Order.Commands
{
    public record ExpiredOrderCommand : IRequest<Unit>;
}
