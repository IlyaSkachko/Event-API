using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IGetAllParticipantUseCase
    {
        Task<IEnumerable<ParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
