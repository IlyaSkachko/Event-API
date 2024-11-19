using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetByLocationEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken);
    }
}
