using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Delete
{
    public class DeleteParticipantUseCase : IDeleteParticipantUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParticipantUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(id, cancellationToken);

            await unitOfWork.ParticipantRepository.DeleteAsync(participant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
