using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Get
{
    public class GetByNameEventUseCase : IGetByNameEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByNameEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<EventDTO> ExecuteAsync(string name, CancellationToken cancellationToken)
        {
            if (name is null)
            {
                throw new BadRequestException("Event name is missing");
            }

            var _event = await unitOfWork.EventRepository.GetByNameAsync(name, cancellationToken);

            if (_event == null )
            {
                throw new NotFoundException("Event is not found");
            }

            return mapper.Map<EventDTO>(_event);
        }
    }
}
