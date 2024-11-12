using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Get.Interfaces
{
    public interface IGetAllParticipantUseCase
    {
        Task<IEnumerable<ParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
