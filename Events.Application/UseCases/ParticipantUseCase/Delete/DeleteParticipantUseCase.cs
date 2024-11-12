using Events.Application.Exceptions;
using Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces;
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

            if (participant is null)
            {
                throw new BadRequestException("Invalid delete operation! This participant does not exist");
            }

            try
            {
                await unitOfWork.ParticipantRepository.DeleteAsync(participant, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid delete operation! This participant is not found");
            }
        }
    }
}
