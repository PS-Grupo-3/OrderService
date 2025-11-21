using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Adapter
{
    public interface IEventServiceClient
    {
        Task MarkSeatsAsUnavailableAsync(Guid eventId, IEnumerable<Guid> eventSeatIds, CancellationToken ct = default);
        Task MarkSeatsAsAvailableAsync(Guid eventId, IEnumerable<Guid> eventSeatIds, CancellationToken ct = default);

        Task MarkSectorAsUnavailableAsync(Guid eventSectorId, CancellationToken ct = default);
        Task MarkSectorAsAvailableAsync(Guid eventSectorId, CancellationToken ct = default);
    }
}
