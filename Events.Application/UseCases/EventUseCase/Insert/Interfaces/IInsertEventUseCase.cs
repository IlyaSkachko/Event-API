using Events.Application.DTO.Event;

namespace Events.Application.UseCases.EventUseCase.Insert.Interfaces
{
    public interface IInsertEventUseCase
    {
        Task ExecuteAsync(EventDTO dto, CancellationToken cancellationToken);
    }
}
