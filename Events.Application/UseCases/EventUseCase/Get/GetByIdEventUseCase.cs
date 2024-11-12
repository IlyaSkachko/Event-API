using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Get
{
    public class GetByIdEventUseCase : IGetByIdEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        
        public GetByIdEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<EventDTO> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var _event = await unitOfWork.EventRepository.GetByIdAsync(id, cancellationToken);

                return mapper.Map<EventDTO>(_event);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Event is not found");
            }
        }
    }
}
