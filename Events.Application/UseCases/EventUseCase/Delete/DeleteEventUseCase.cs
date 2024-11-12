using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Delete.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Delete
{
    public class DeleteEventUseCase : IDeleteEventUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteEventUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var _event = await unitOfWork.EventRepository.GetByIdAsync(id, cancellationToken);

            if (_event is null)
            {
                throw new BadRequestException("Invalid delete operation! This participant does not exist");
            }

            try
            {
                await unitOfWork.EventRepository.DeleteAsync(_event, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid delete operation! This participant is not found");
            }
        }
    }
}
