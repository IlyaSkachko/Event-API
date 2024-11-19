using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IGetByRefreshTokenParticipantUseCase
    {
        Task<ParticipantDTO> ExecuteAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
