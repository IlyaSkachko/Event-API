using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;

namespace Events.Application.UseCases.ParticipantUseCase.Login.Interfaces
{
    public interface ILoginParticipantUseCase
    {
        Task<TokenDTO> ExecuteAsync(ParticipantAuthDTO dto, CancellationToken cancellationToken);
    }
}
