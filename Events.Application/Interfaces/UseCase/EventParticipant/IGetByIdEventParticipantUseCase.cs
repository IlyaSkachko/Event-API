using Events.Application.DTO.EventParticipant;

namespace Events.Application.Interfaces.UseCase.EventParticipant
{
    public interface IGetByIdEventParticipantUseCase
    {
        Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int eventId, CancellationToken cancellationToken);
    }
}
