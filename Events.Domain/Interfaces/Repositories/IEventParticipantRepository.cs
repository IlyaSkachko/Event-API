using Events.Domain.Models;

namespace Events.Domain.Interfaces.Repositories
{
    public interface IEventParticipantRepository : IGeneralRepository<EventParticipant>
    {
        Task<EventParticipant> GetByIdAsync(int eventId, int participantId, CancellationToken cancellationToken);
        Task<List<EventParticipant>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken);
    }
}
