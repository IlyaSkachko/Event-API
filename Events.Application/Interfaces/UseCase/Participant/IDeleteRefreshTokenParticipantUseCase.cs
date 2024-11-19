using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IDeleteRefreshTokenParticipantUseCase
    {
        Task ExecuteAsync(ParticipantDTO dto, CancellationToken cancellationToken);
    }
}
