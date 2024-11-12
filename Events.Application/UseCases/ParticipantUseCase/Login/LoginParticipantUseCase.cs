using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;
using Events.Application.Exceptions;
using Events.Application.UseCases.HashUseCase.Hash.Interfaces;
using Events.Application.UseCases.HashUseCase.Verify.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Login.Interfaces;
using Events.Application.UseCases.TokenUseCase.Generate.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Login
{
    public class LoginParticipantUseCase : ILoginParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashPasswordUseCase hashPasswordUseCase;
        private readonly IVerifyPasswordUseCase verifyPasswordUseCase;
        private readonly ITokenGenerateUseCase tokenGenerateUseCase;

        public LoginParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork, IHashPasswordUseCase hashPasswordUseCase, 
            IVerifyPasswordUseCase verifyPasswordUseCase, ITokenGenerateUseCase tokenGenerateUseCase)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.hashPasswordUseCase = hashPasswordUseCase;
            this.verifyPasswordUseCase = verifyPasswordUseCase;
            this.tokenGenerateUseCase = tokenGenerateUseCase;
        }

        public async Task<TokenDTO> ExecuteAsync(ParticipantAuthDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Auth data is missing");
            }

            var participant = await unitOfWork.ParticipantRepository.GetByEmailAsync(dto.Email, cancellationToken);

            if (participant is null)
            {
                throw new NotFoundException("Participant is not exist");
            }

            var participantDTO = mapper.Map<ParticipantDTO>(participant);

            if (!verifyPasswordUseCase.Execute(dto.Password, participantDTO.Password, participantDTO.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid password! Access denied");
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);
            var refreshExpires = DateTime.UtcNow.AddDays(7);

            var accessToken = tokenGenerateUseCase.Execute(participantDTO, accessExpires);
            var refreshToken = tokenGenerateUseCase.Execute(participantDTO, refreshExpires);

            participant.RefreshToken = refreshToken;

            try
            {
                await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);
            }
            catch(InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! Participant not found!");
            }

            return new TokenDTO
            {
                Access = accessToken,
                AccessExpires = accessExpires,
                Refresh = refreshToken,
                RefreshExpires = refreshExpires
            };
        }
    }
}
