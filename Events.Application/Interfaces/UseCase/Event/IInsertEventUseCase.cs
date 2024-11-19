using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IInsertEventUseCase
    {
        Task ExecuteAsync(EventDTO dto, CancellationToken cancellationToken);
    }
}
