using Events.Application.DTO.EventParticipant;

namespace Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces
{
    public interface IGetAllEventParticipantUseCase
    {
        Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
