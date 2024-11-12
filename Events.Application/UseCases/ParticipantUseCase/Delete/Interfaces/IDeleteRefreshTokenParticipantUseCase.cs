using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces
{
    public interface IDeleteRefreshTokenParticipantUseCase
    {
        Task ExecuteAsync(ParticipantDTO dto, CancellationToken cancellationToken);
    }
}
