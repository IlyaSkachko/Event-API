namespace Events.Application.Interfaces.UseCase.EventParticipant
{
    public interface IDeleteEventParticipantUseCase
    {
        Task ExecuteAsync(int eventId, int participantId, CancellationToken cancellationToken);
    }
}
