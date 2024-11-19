using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Get
{
    public class GetByIdParticipantUseCase : IGetByIdParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByIdParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ParticipantDTO> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(id, cancellationToken);

            return mapper.Map<ParticipantDTO>(participant);
        }
    }
}
