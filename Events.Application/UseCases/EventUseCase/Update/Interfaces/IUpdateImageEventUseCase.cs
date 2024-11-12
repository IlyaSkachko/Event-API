namespace Events.Application.UseCases.EventUseCase.Update.Interfaces
{
    public interface IUpdateImageEventUseCase
    {
        Task ExecuteAsync(int eventId, string url, CancellationToken cancellationToken);
    }
}
