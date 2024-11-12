using Events.Application.Exceptions;
using Events.Application.UseCases.EventParticipantUseCase.Delete.Interfaces;
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

            if (eventParticipant is null)
            {
                throw new BadRequestException("Invalid delete operation! This participant does not exist in this event");
            }

            try
            {
                await unitOfWork.EventParticipantRepository.DeleteAsync(eventParticipant, cancellationToken);
            }
            catch(InvalidOperationException)
            {
                throw new NotFoundException("Invalid delete operation! This event participant is not found");
            }
        }
    }
}
