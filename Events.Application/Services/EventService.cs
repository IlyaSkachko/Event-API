using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.Services.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public EventService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;   
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(EventDTO dto, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<Event>(dto);

            await unitOfWork.EventRepository.InsertAsync(_event, cancellationToken);
        }

        public async Task AddImageAsync(EventImageDTO dto, CancellationToken cancellationToken)
        {
            await unitOfWork.EventRepository.AddImageAsync(dto.Id, dto.Image, cancellationToken);
        }

        public async Task DeleteAsync(EventDTO dto, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<Event>(dto);

            await unitOfWork.EventRepository.DeleteAsync(_event, cancellationToken);
        }

        public async Task<IEnumerable<EventDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<IEnumerable<EventDTO>> GetByCategoryAsync(CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByCategoryAsync(categoryDTO.Id, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<IEnumerable<EventDTO>> GetByCategoryAsync(int pageNumber, int pageSize, CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByCategoryAsync(pageNumber, pageSize, categoryDTO.Id, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<IEnumerable<EventDTO>> GetByDateAsync(DateTime dateTime, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByDateAsync(dateTime, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<IEnumerable<EventDTO>> GetByDateAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByDateAsync(pageNumber, pageSize, dateTime, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<EventDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var _event = await unitOfWork.EventRepository.GetByIdAsync(id, cancellationToken);

            return mapper.Map<EventDTO>(_event);
        }

        public async Task<IEnumerable<EventDTO>> GetByLocationAsync(string location, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByLocationAsync(location, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<IEnumerable<EventDTO>> GetByLocationAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventRepository.GetByLocationAsync(pageNumber, pageSize, location, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }

        public async Task<EventDTO> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var _event = await unitOfWork.EventRepository.GetByNameAsync(name, cancellationToken);

            return mapper.Map<EventDTO>(_event);
        }

        public async Task UpdateAsync(EventDTO dto, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<Event>(dto);

            await unitOfWork.EventRepository.UpdateAsync(_event, cancellationToken);
        }
    }
}

