using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface ILoginParticipantUseCase
    {
        Task ExecuteAsync(ParticipantAuthDTO dto, string refreshToken, CancellationToken cancellationToken);
    }
}
