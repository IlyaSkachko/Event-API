namespace Events.Application.UseCases.EventUseCase.Delete.Interfaces
{
    public interface IDeleteEventUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
