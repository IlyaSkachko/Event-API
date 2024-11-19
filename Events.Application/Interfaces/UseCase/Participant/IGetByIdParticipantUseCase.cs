using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IGetByIdParticipantUseCase
    {
        Task<ParticipantDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
