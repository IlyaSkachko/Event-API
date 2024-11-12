using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Update.Interfaces
{
    public interface IUpdateParticipantUseCase
    {
        Task ExecuteAsync(UpdateParticipantDTO dto, CancellationToken cancellationToken);
    }
}
