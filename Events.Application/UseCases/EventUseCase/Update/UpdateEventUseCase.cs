using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Update.Interfaces;
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
            if (dto is null)
            {
                throw new BadRequestException("Event participant data is missing");
            }

            try
            {
                var _event = mapper.Map<Event>(dto);

                await unitOfWork.EventRepository.UpdateAsync(_event, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! This event participant does not exist");
            }
        }
    }
}
