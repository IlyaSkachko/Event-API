using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Get.Interfaces
{
    public interface IGetByRefreshTokenParticipantUseCase
    {
        Task<ParticipantDTO> ExecuteAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
