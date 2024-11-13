using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.UseCases.ParticipantUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Get
{
    public class GetAllParticipantUseCase : IGetAllParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.ParticipantRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            if (collection is null)
            {
                throw new NotFoundException("Participants are not found");
            }

            return mapper.Map<IEnumerable<ParticipantDTO>>(collection);
        }
    }
}