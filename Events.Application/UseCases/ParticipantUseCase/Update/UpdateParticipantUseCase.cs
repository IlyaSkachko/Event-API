using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.UseCases.ParticipantUseCase.Update.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.ParticipantUseCase.Update
{
    public class UpdateParticipantUseCase : IUpdateParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateParticipantDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Participant data is missing");
            }

            try
            {
                var entity = mapper.Map<Participant>(dto);

                await unitOfWork.ParticipantRepository.UpdateAsync(entity, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! This participant does not exist");
            }
        }
    }
}
