using Events.Application.DTO.Event;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetByIdEventUseCase
    {
        Task<EventDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
