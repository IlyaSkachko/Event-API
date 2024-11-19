using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IUpdateParticipantUseCase
    {
        Task ExecuteAsync(UpdateParticipantDTO dto, CancellationToken cancellationToken);
    }
}
