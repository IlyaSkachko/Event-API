using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.ParticipantUseCase.Get.Interfaces
{
    public interface IGetByIdParticipantUseCase
    {
        Task<ParticipantDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
