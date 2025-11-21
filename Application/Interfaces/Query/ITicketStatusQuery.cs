using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicketStatus
{
    public interface ITicketStatusQuery
    {
        Task<List<TicketStatus>> GetAllTicketStatusAsync();
        Task<TicketStatus?> GetTicketStatusById(int statusId);
    }
}
