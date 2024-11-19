using Events.Application.DTO.EventParticipant;

namespace Events.Application.Interfaces.UseCase.EventParticipant
{
    public interface IUpdateEventParticipantUseCase
    {
        Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
    }
}
