using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IInsertParticipantUseCase
    {
        Task ExecuteAsync(CreateParticipantDTO dto, byte[] salt, CancellationToken cancellationToken);
    }
}
