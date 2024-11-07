using Events.Application.DTO.EventParticipant;

namespace Events.Application.Services.Interfaces
{
    public interface IEventParticipantService
    {
        Task<IEnumerable<EventParticipantDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<EventParticipantDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<EventParticipantDTO>> GetByIdAsync(int eventId, CancellationToken cancellationToken);
        Task UpdateAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(int eventId, int participantId, CancellationToken cancellationToken);
        Task InsertAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
    }
}
