using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetByDateEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken);
    }
}
