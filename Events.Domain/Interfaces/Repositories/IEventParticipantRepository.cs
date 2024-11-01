using Events.Domain.Models;

namespace Events.Domain.Interfaces.Repositories
{
    public interface IEventParticipantRepository : IGeneralRepository<EventParticipant>
    {
        Task<EventParticipant> GetByIdAsync(int eventId, CancellationToken cancellationToken);
    }
}
