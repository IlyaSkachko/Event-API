namespace Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces
{
    public interface IDeleteParticipantUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
