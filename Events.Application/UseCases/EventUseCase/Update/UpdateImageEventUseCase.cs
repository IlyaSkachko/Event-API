using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Update.Interfaces;
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
            try
            {
                await unitOfWork.EventRepository.AddImageAsync(eventId, url, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid update image operation! Event doesn't exist");
            }
        }
    }
}
