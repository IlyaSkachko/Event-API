using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Get.Interfaces
{
    public interface IGetByIdEventUseCase
    {
        Task<EventDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
