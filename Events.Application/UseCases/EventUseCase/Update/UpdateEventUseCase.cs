using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.EventUseCase.Update
{
    public class UpdateEventUseCase : IUpdateEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(EventDTO dto, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<Event>(dto);

            await unitOfWork.EventRepository.UpdateAsync(_event, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
