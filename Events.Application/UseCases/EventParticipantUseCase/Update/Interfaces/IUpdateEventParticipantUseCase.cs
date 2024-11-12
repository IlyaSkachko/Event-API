using Events.Application.DTO.EventParticipant;

namespace Events.Application.UseCases.EventParticipantUseCase.Update.Interfaces
{
    public interface IUpdateEventParticipantUseCase
    {
        Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
    }
}
