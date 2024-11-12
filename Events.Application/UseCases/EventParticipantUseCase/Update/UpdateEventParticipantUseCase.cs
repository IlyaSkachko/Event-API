using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventParticipantUseCase.Update.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.EventParticipantUseCase.Update
{
    public class UpdateEventParticipantUseCase : IUpdateEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Event participant data is missing");
            }

            try
            {
                var obj = mapper.Map<EventParticipant>(dto);

                await unitOfWork.EventParticipantRepository.UpdateAsync(obj, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! This event participant does not exist");
            }
        }
    }
}
