using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
using Events.Domain.Interfaces.UOW;
using Microsoft.Extensions.Caching.Memory;

namespace Events.Application.UseCases.EventUseCase.Get
{
    public class GetByIdEventUseCase : IGetByIdEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;
        
        public GetByIdEventUseCase(IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }

        public async Task<EventDTO> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue($"event:{id}", out EventDTO cachedEvent))
            {
                return cachedEvent;
            }

            var _event = await unitOfWork.EventRepository.GetByIdAsync(id, cancellationToken);

            var eventDto = mapper.Map<EventDTO>(_event);

            memoryCache.Set($"event:{id}", eventDto, TimeSpan.FromMinutes(30));

            return eventDto;
        }
    }
}
