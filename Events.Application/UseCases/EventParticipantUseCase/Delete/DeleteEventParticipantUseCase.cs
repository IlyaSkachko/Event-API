using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.EventParticipant;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventParticipantUseCase.Delete
{
    public class DeleteEventParticipantUseCase : IDeleteEventParticipantUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteEventParticipantUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int eventId, int participantId, CancellationToken cancellationToken)
        {
            var eventParticipant = await unitOfWork.EventParticipantRepository.GetByIdAsync(eventId, participantId, cancellationToken);

            await unitOfWork.EventParticipantRepository.DeleteAsync(eventParticipant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
