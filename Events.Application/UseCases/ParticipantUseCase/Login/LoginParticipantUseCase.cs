using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Interfaces.UseCase.Hash;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Application.Interfaces.UseCase.Token;
using Events.Domain.Interfaces.UOW;
using Microsoft.AspNetCore.Http;

namespace Events.Application.UseCases.ParticipantUseCase.Login
{
    public class LoginParticipantUseCase : ILoginParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVerifyPasswordUseCase verifyPasswordUseCase;
        private readonly ITokenGenerateUseCase tokenGenerateUseCase;
        private readonly IGetByEmailParticipantUseCase getByEmailParticipantUseCase;
        public LoginParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork, IVerifyPasswordUseCase verifyPasswordUseCase, ITokenGenerateUseCase tokenGenerateUseCase,
            IGetByEmailParticipantUseCase getByEmailParticipantUseCase)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.verifyPasswordUseCase = verifyPasswordUseCase;
            this.tokenGenerateUseCase = tokenGenerateUseCase;
            this.getByEmailParticipantUseCase = getByEmailParticipantUseCase;
        }

        public async Task ExecuteAsync(HttpContext httpContext, ParticipantAuthDTO dto, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByEmailAsync(dto.Email, cancellationToken);

            if (!verifyPasswordUseCase.Execute(dto.Password, participant.Password, participant.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid password! Access denied");
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);

            var refreshExpires = DateTime.UtcNow.AddDays(7);

            var participantDTO = mapper.Map<ParticipantDTO>(participant);

            var accessToken = tokenGenerateUseCase.Execute(participantDTO, accessExpires);

            var refreshToken = tokenGenerateUseCase.Execute(participantDTO, refreshExpires);

            participant.RefreshToken = refreshToken;

            await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = accessExpires
            };

            httpContext.Response.Cookies.Append("access-token", accessToken, accessCookieOptions);

            var refreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshExpires
            };

            httpContext.Response.Cookies.Append("refresh-token", refreshToken, refreshCookieOptions);

        }
    }
}
