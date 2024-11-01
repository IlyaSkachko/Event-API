using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Services.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    
        public async Task DeleteAsync(ParticipantDTO dto, CancellationToken cancellationToken)
        {
            var obj = mapper.Map<Participant>(dto);

            await unitOfWork.ParticipantRepository.DeleteAsync(obj, cancellationToken);
        }

        public async Task<IEnumerable<ParticipantDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.ParticipantRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<ParticipantDTO>>(collection);
        }

        public async Task<IEnumerable<ParticipantDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.ParticipantRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return mapper.Map<IEnumerable<ParticipantDTO>>(collection);
        }

        public async Task<ParticipantDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(id, cancellationToken);

            return mapper.Map<ParticipantDTO>(participant);
        }

        public async Task InsertAsync(ParticipantDTO dto, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Participant>(dto);

            await unitOfWork.ParticipantRepository.InsertAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(ParticipantDTO dto, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Participant>(dto);

            await unitOfWork.ParticipantRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
