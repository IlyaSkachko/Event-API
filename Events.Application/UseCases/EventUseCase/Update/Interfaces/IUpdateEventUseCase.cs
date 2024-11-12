using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Update.Interfaces
{
    public interface IUpdateEventUseCase
    {
        Task ExecuteAsync(EventDTO dto, CancellationToken cancellationToken);
    }
}
