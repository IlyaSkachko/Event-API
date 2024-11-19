using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.EventParticipant;
using Events.Application.Validation.EventParticipant;
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
            var validator = new EventParticipantValidator();

            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var eventParticipant = mapper.Map<EventParticipant>(dto);

            await unitOfWork.EventParticipantRepository.InsertAsync(eventParticipant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
