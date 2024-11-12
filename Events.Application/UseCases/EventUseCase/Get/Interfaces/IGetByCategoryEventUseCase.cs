using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Get.Interfaces
{
    public interface IGetByCategoryEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, int id, CancellationToken cancellationToken);
    }
}
