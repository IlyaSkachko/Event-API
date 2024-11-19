using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
using Events.Domain.Interfaces.UOW;
using Microsoft.Extensions.Caching.Memory;

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
            var _event = await unitOfWork.EventRepository.GetByNameAsync(name, cancellationToken);

            return mapper.Map<EventDTO>(_event);
        }
    }
}
