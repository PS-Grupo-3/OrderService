using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicketSector
{
    public interface ITicketSectorQuery
    {
        Task<IEnumerable<TicketSector>> GetSectorsByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
    }
}
