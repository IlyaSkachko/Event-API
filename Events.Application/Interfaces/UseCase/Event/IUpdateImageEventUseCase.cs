namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IUpdateImageEventUseCase
    {
        Task ExecuteAsync(int eventId, string url, CancellationToken cancellationToken);
    }
}
