namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IDeleteParticipantUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
