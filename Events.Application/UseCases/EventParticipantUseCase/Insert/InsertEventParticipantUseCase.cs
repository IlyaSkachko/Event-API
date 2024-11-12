using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventParticipantUseCase.Insert.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.EventParticipantUseCase.Insert
{
    public class InsertEventParticipantUseCase : IInsertEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public InsertEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Invalid insert operation! Event partipant data is missing");
            }

            try
            {
                var eventParticipant = mapper.Map<EventParticipant>(dto);

                await unitOfWork.EventParticipantRepository.InsertAsync(eventParticipant, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new AlreadyExistException("Invalid insert operation! This event participant already exist");
            }
        }
    }
}
