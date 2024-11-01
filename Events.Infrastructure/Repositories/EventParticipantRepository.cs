using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class EventParticipantRepository : GeneralRepository<EventParticipant>, IEventParticipantRepository
    {
        public EventParticipantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<EventParticipant> GetByIdAsync(int eventId, CancellationToken cancellationToken)
        {
            return (await table.Where(e => e.EventId == eventId).ToListAsync(cancellationToken)).FirstOrDefault();
        }
    }
}
