using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IGetByEmailParticipantUseCase
    {
        Task<ParticipantDTO> ExecuteAsync(ParticipantAuthDTO participant, CancellationToken cancellationToken);
    }
}
