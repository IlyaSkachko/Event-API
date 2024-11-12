using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.UseCases.EventUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Get
{
    public class GetByCategoryEventUseCase : IGetByCategoryEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByCategoryEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, int id, CancellationToken cancellationToken)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                throw new BadRequestException("Negative page option");
            }

            var collection = await unitOfWork.EventRepository.GetByCategoryAsync(pageNumber, pageSize, id, cancellationToken);

            if (collection is null)
            {
                throw new NotFoundException("Events are not found");
            }

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }
    }
}
