namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IDeleteEventUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
