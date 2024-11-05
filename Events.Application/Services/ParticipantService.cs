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
        private readonly IHashService hashService;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper, IHashService hashService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hashService = hashService;
        }
    
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var obj = await unitOfWork.ParticipantRepository.GetByIdAsync(id, cancellationToken);

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

        public async Task InsertAsync(CreateParticipantDTO dto, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Participant>(dto);

            entity.Password = hashService.HashPassword(entity.Password, out byte[] salt);
            entity.PasswordSalt = salt;

            await unitOfWork.ParticipantRepository.InsertAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(UpdateParticipantDTO dto, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Participant>(dto);

            var existingEntity = await unitOfWork.ParticipantRepository.GetByIdAsync(dto.Id, cancellationToken);

            if (existingEntity != null)
            {
                mapper.Map(dto, existingEntity);

                await unitOfWork.ParticipantRepository.UpdateAsync(existingEntity, cancellationToken);
            }
        }
    }
}
