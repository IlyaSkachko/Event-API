using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventParticipantUseCase.Get
{
    public class GetByIdEventParticipantUseCase : IGetByIdEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByIdEventParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int eventId, CancellationToken cancellationToken)
        {
            var eventParticipants = await unitOfWork.EventParticipantRepository.GetByEventIdAsync(eventId, cancellationToken);

            if (eventParticipants is null)
            {
                throw new NotFoundException("Event has no participants");
            }
            
            return mapper.Map<IEnumerable<EventParticipantDTO>>(eventParticipants);
        }
    }
}
