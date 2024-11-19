using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Participant;
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
            var entity = mapper.Map<Participant>(dto);

            await unitOfWork.ParticipantRepository.UpdateAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
