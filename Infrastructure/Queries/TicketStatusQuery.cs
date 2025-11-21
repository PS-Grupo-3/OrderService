using Application.Interfaces.ITicketStatus;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class TicketStatusQuery : ITicketStatusQuery
    {
        private readonly AppDbContext _context;

        public TicketStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketStatus>> GetAllTicketStatusAsync()
        {
            return await _context.TicketStatuses.ToListAsync();
        }

        public async Task<TicketStatus?> GetTicketStatusById(int statusId)
        {
            return await _context.TicketStatuses.FirstOrDefaultAsync(status => status.StatusID == statusId);
        }
    }
}
