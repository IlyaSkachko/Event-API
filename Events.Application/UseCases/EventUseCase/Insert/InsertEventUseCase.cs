using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Insert.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.EventUseCase.Insert
{
    public class InsertEventUseCase : IInsertEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public InsertEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(EventDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Event data is missing");
            }

            try
            {
                var _event = mapper.Map<Event>(dto);

                await unitOfWork.EventRepository.InsertAsync(_event, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new AlreadyExistException("Invalid insert operation! This event already exist");
            }
        }
    }
}
