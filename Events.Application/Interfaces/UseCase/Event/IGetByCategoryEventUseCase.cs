using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetByCategoryEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, int id, CancellationToken cancellationToken);
    }
}
