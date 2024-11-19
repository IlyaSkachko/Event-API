using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetByNameEventUseCase
    {
        Task<EventDTO> ExecuteAsync(string name, CancellationToken cancellationToken);
    }
}
