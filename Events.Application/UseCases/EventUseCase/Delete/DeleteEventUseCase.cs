using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
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

            await unitOfWork.EventRepository.DeleteAsync(_event, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
