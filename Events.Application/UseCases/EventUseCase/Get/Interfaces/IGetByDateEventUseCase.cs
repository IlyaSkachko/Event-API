using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Get.Interfaces
{
    public interface IGetByDateEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken);
    }
}
