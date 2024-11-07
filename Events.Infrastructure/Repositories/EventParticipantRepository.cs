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

        public async Task<EventParticipant> GetByIdAsync(int eventId, int participantId, CancellationToken cancellationToken)
        {
            return await table.Where(e => e.EventId == eventId && e.ParticipantId == participantId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<EventParticipant>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken)
        {
            return await table.Where(ep => ep.EventId == eventId).ToListAsync(cancellationToken);
        }
    }
}
