using Events.Application.DTO.EventParticipant;

namespace Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces
{
    public interface IGetByIdEventParticipantUseCase
    {
        Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int eventId, CancellationToken cancellationToken);
    }
}
