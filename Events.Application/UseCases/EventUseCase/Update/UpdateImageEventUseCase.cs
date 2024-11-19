using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Update
{
    public class UpdateImageEventUseCase : IUpdateImageEventUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateImageEventUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int eventId, string url, CancellationToken cancellationToken)
        {
            await unitOfWork.EventRepository.AddImageAsync(eventId, url, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
