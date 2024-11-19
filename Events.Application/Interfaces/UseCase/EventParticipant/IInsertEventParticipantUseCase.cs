using Events.Application.DTO.EventParticipant;

namespace Events.Application.Interfaces.UseCase.EventParticipant
{
    public interface IInsertEventParticipantUseCase
    {
        Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
    }
}
