using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Get.Interfaces
{
    public interface IGetByLocationEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken);
    }
}
