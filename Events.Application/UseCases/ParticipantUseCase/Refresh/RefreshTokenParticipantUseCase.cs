using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Application.Interfaces.UseCase.Token;
using Events.Application.UseCases.ParticipantUseCase.Delete;
using Events.Application.UseCases.ParticipantUseCase.Get;
using Events.Application.UseCases.TokenUseCase.Generate;
using Events.Application.UseCases.TokenUseCase.Validation;
using Events.Domain.Interfaces.UOW;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;

namespace Events.Application.UseCases.ParticipantUseCase.Refresh
{
    public class RefreshTokenParticipantUseCase : IRefreshTokenParticipantUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ITokenGenerateUseCase tokenGenerateUseCase;

        public RefreshTokenParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper, ITokenGenerateUseCase tokenGenerateUseCase)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.tokenGenerateUseCase = tokenGenerateUseCase;
        }


        public async Task ExecuteAsync(HttpContext httpContext, CancellationToken cancellationToken)
        {
            var refreshToken = httpContext.Request.Cookies["refresh-token"];

            var participant = mapper.Map<ParticipantDTO>(await unitOfWork.ParticipantRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken));

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadToken(refreshToken) as JwtSecurityToken;

            if (jwtToken == null || jwtToken.ValidTo < DateTime.UtcNow)
            {
                httpContext.Response.Cookies.Delete("access-token");

                httpContext.Response.Cookies.Delete("refresh-token");

                var entity = await unitOfWork.ParticipantRepository.GetByIdAsync(participant.Id, cancellationToken);

                entity.RefreshToken = "";

                await unitOfWork.ParticipantRepository.UpdateAsync(entity, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                throw new UnauthorizedAccessException("Refresh token has expired");
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);

            var newAccessToken = tokenGenerateUseCase.Execute(participant, accessExpires);

            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = accessExpires
            };

            httpContext.Response.Cookies.Append("access-token", newAccessToken, accessCookieOptions);
        }
    }
}
