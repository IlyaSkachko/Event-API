namespace Events.Application.UseCases.EventParticipantUseCase.Delete.Interfaces
{
    public interface IDeleteEventParticipantUseCase
    {
        Task ExecuteAsync(int eventId, int participantId, CancellationToken cancellationToken);
    }
}
