using Events.Application.DTO.EventParticipant;

namespace Events.Application.UseCases.EventParticipantUseCase.Insert.Interfaces
{
    public interface IInsertEventParticipantUseCase
    {
        Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken);
    }
}
