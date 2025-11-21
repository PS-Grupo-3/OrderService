using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicketSector
{
    public interface ITicketSectorCommand
    {
        Task InsertTicketSectorAsync(TicketSector ticketSector, CancellationToken cancellationToken = default);
        Task InsertTicketSectorRangeAsync(IEnumerable<TicketSector> ticketSectors,CancellationToken cancellationToken = default);
    }
}
