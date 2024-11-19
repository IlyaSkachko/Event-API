using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.ParticipantUseCase.Get
{
    public class GetByEmailParticipantUseCase : IGetByEmailParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByEmailParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ParticipantDTO> ExecuteAsync(ParticipantAuthDTO participant, CancellationToken cancellationToken)
        {
            var obj = await unitOfWork.ParticipantRepository.GetByEmailAsync(participant.Email, cancellationToken);

            return mapper.Map<ParticipantDTO>(obj);
        }
    }
}
