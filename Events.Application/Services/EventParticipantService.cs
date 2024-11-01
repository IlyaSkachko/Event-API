using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Services.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;
using System.IO.Pipes;

namespace Events.Application.Services
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EventParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            var category = mapper.Map<EventParticipant>(dto);

            await unitOfWork.EventParticipantRepository.DeleteAsync(category, cancellationToken);
        }

        public async Task<IEnumerable<EventParticipantDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventParticipantRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<EventParticipantDTO>>(collection);
        }

        public async Task<IEnumerable<EventParticipantDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.EventParticipantRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return mapper.Map<IEnumerable<EventParticipantDTO>>(collection);
        }

        public async Task<EventParticipantDTO> GetByIdAsync(int eventId, CancellationToken cancellationToken)
        {
            var obj = await unitOfWork.EventParticipantRepository.GetByIdAsync(eventId, cancellationToken);

            return mapper.Map<EventParticipantDTO>(obj);
        }

        public async Task InsertAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            var obj = mapper.Map<EventParticipant>(dto);

            await unitOfWork.EventParticipantRepository.InsertAsync(obj, cancellationToken);
        }

        public async Task UpdateAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            var obj = mapper.Map<EventParticipant>(dto);

            await unitOfWork.EventParticipantRepository.UpdateAsync(obj, cancellationToken);
        }
    }
}
