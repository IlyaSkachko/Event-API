using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Insert.Interfaces
{
    public interface IInsertParticipantUseCase
    {
        Task ExecuteAsync(CreateParticipantDTO dto, CancellationToken cancellationToken);
    }
}
