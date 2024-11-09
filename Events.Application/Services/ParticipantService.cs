using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;
using Events.Application.Services.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;
using System.Runtime.InteropServices;

namespace Events.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHashService hashService;
        private readonly ITokenService tokenService;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper, IHashService hashService, ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hashService = hashService;
            this.tokenService = tokenService;
        }

        public async Task<TokenDTO> Login(ParticipantAuthDTO dto, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByEmailAsync(dto.Email, cancellationToken);

            var participantDTO = mapper.Map<ParticipantDTO>(participant);

            if (!hashService.VerifyPassword(dto.Password, participantDTO.Password, participantDTO.PasswordSalt))
            {
                throw new UnauthorizedAccessException();
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);
            var refreshExpires = DateTime.UtcNow.AddDays(7);

            var accessToken = tokenService.GenerateAccessToken(participantDTO, accessExpires);
            var refreshToken = tokenService.GenerateRefreshToken(participantDTO, refreshExpires);

            participant.RefreshToken = refreshToken; 

            await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);

            return new TokenDTO 
            {
                Access = accessToken,
                AccessExpires = accessExpires,
                Refresh = refreshToken,
                RefreshExpires = refreshExpires
            };
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
                mapper.Map<Participant>(dto);

                await unitOfWork.ParticipantRepository.UpdateAsync(existingEntity, cancellationToken);
            }
        }

        public async Task<ParticipantDTO> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

            return mapper.Map<ParticipantDTO>(participant);
        }

        public async Task DeleteRefreshTokenAsync(ParticipantDTO dto, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(dto.Id, cancellationToken);

            participant.RefreshToken = "";

            await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);
        }
    }
}
