using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventParticipantUseCase.Get
{
    public class GetAllEventParticipantUseCase : IGetAllEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventParticipantRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            if (collection is null)
            {
                throw new NotFoundException("Event participants is not found");
            }

            return mapper.Map<IEnumerable<EventParticipantDTO>>(collection);
        }
    }
}
