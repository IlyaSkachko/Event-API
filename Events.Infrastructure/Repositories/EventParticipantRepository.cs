using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Domain.Interfaces.Repositories;

namespace Events.Infrastructure.Repositories
{
    public class EventParticipantRepository : GeneralRepository<EventParticipant>, IEventParticipantRepository
    {
        public EventParticipantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
