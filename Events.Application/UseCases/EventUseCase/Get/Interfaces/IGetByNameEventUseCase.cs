using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Get.Interfaces
{
    public interface IGetByNameEventUseCase
    {
        Task<EventDTO> ExecuteAsync(string name, CancellationToken cancellationToken);
    }
}
