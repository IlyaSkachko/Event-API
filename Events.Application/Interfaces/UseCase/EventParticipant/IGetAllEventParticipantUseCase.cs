using Events.Application.DTO.EventParticipant;

namespace Events.Application.Interfaces.UseCase.EventParticipant
{
    public interface IGetAllEventParticipantUseCase
    {
        Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
